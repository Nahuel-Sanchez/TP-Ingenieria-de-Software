using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using DAL_08YS.Interfaces_Repositories.Negocio.cargos;
using Service_08YS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL_08YS.Negocio
{
    /// <summary>
    /// Cambio de habitación de una reserva: upgrade pedido por el huésped (con cargo, solo con la estadía en curso)
    /// o reubicación por motivos del hotel (sin cargo; también antes del check-in). Las reglas de cada motivo
    /// viven en su política.
    /// Al registrar, la BLL valida lo que depende solo de la solicitud (permiso y datos del motivo) y
    /// sp_RegistrarCambioHabitacion valida el estado de la base y registra en una sola llamada y una sola transacción.
    /// Solo usa su propio repositorio: la reserva la pide a ReservaBLL_68SA (que es la dueña del repositorio de reservas).
    /// </summary>
    public class CambioHabitacionBLL_68SA
    {
        #region Campos

        private readonly ICambioHabitacionRepository_68SA _cambioRepo;
        private readonly ReservaBLL_68SA _reservaBll;
        private readonly BitacoraBLL_08YS _bitacoraBll;
        private readonly Dictionary<MotivoCambioHabitacion, IPoliticaCambioHabitacion_68SA> _politicas;

        #endregion

        public CambioHabitacionBLL_68SA(ICambioHabitacionRepository_68SA cambioRepo, ReservaBLL_68SA reservaBll,
                                        BitacoraBLL_08YS bitacoraBll, IEnumerable<IPoliticaCambioHabitacion_68SA> politicas)
        {
            _cambioRepo = cambioRepo;
            _reservaBll = reservaBll;
            _bitacoraBll = bitacoraBll;
            _politicas = politicas.ToDictionary(p => p.Motivo);
        }

        #region Consultas

        /// <summary>Noches que faltan desde hoy hasta el egreso (las que todavía no se durmieron).</summary>
        public static int CalcularNochesRestantes(DateTime fechaEgreso, DateTime hoy)
            => Math.Max(0, (fechaEgreso.Date - hoy.Date).Days);

        /// <summary>Noches que faltan de la reserva: desde hoy o desde el ingreso si todavía no llegó.</summary>
        public static int CalcularNochesRestantes(Reserva_68SA reserva, DateTime hoy)
        {
            DateTime desde = reserva.FechaIngreso.Date > hoy.Date ? reserva.FechaIngreso.Date : hoy.Date;
            return CalcularNochesRestantes(reserva.FechaEgreso, desde);
        }

        /// <summary>Habitaciones a las que se puede mudar al huésped según el motivo.</summary>
        public List<Habitacion_68SA> GetOpciones(int reservaId, MotivoCambioHabitacion motivo)
        {
            var politica = Politica(motivo);
            SessionManager_08YS.Instance.ValidatePermission(politica.PermisoRequerido);
            ObtenerReservaModificable(reservaId, politica);
            return _cambioRepo.GetHabitacionesParaCambio(reservaId, politica.SoloCategoriaSuperior);
        }

        public List<CambioHabitacion_68SA> GetHistorial(int reservaId) => _cambioRepo.GetByReserva(reservaId);

        #endregion

        /// <summary>
        /// Registra el cambio en una sola ida a la base. Lo que depende de la solicitud (permiso y datos del motivo)
        /// se valida acá; lo que depende del estado de la base (estado de la reserva, habitación libre hasta el egreso,
        /// capacidad, tarifa mayor y noches restantes del upgrade) lo valida el SP en la misma transacción del alta,
        /// con los datos del momento. El SP también calcula el cargo y arma su descripción.
        /// </summary>
        public ResultadoRegistroCambio_68SA Registrar(SolicitudCambioHabitacion_68SA solicitud)
        {
            if (solicitud == null) throw new ArgumentNullException(nameof(solicitud));

            var politica = Politica(solicitud.Motivo);
            SessionManager_08YS.Instance.ValidatePermission(politica.PermisoRequerido);
            politica.ValidarSolicitud(solicitud);

            var resultado = _cambioRepo.Registrar(solicitud);
            LanzarSiFallo(resultado.Resultado, politica);

            DVManager_08YS.Recalcular();
            _bitacoraBll.RegistrarEvento(politica.EventoBitacora, targetUsername: solicitud.ReservaId.ToString());
            return resultado;
        }

        #region Métodos públicos

        /// <summary>true si el motivo se puede aplicar a una reserva en ese estado (para ofrecerlo o no en pantalla).</summary>
        public bool AdmiteEstado(MotivoCambioHabitacion motivo, EstadoReserva estado)
            => estado == EstadoReserva.EnCurso || (estado == EstadoReserva.Confirmada && Politica(motivo).AdmiteReservaConfirmada);

        /// <summary>true si el usuario actual puede registrar cambios con ese motivo (para mostrar u ocultar opciones).</summary>
        public bool PuedeRegistrar(MotivoCambioHabitacion motivo)
            => SessionManager_08YS.Instance.HasPermission(Politica(motivo).PermisoRequerido);

        /// <summary>Vista previa del cargo (misma fórmula que la base) para mostrarla antes de confirmar.</summary>
        public CotizacionCambioHabitacion_68SA Cotizar(Reserva_68SA reserva, Habitacion_68SA habitacionNueva, MotivoCambioHabitacion motivo)
        {
            if (reserva == null) throw new ReservaNoEncontradaException_68SA();
            if (habitacionNueva?.Tipo == null) throw new HabitacionNoEncontradaException_68SA();

            int noches = CalcularNochesRestantes(reserva, DateTime.Today);
            return Politica(motivo).Cotizar(_reservaBll.GetTarifaVigente(reserva.Id), habitacionNueva.Tipo.TarifaNoche, noches);
        }

        #endregion

        #region Métodos auxiliares

        private IPoliticaCambioHabitacion_68SA Politica(MotivoCambioHabitacion motivo)
        {
            if (!_politicas.TryGetValue(motivo, out var politica))
                throw new DatosInvalidosException_68SA("Motivo de cambio de habitación no válido.");
            return politica;
        }

        private Reserva_68SA ObtenerReservaModificable(int reservaId, IPoliticaCambioHabitacion_68SA politica)
        {
            // ReservaBLL_68SA.GetById ya lanza ReservaNoEncontradaException_68SA si no existe
            var reserva = _reservaBll.GetById(reservaId);
            if (!AdmiteEstado(politica.Motivo, reserva.Estado))
                throw new EstadoReservaInvalidoException_68SA(politica.AdmiteReservaConfirmada
                    ? "Solo se puede reubicar una reserva Confirmada o una estadía En Curso."
                    : "El upgrade solo se puede registrar con la estadía En Curso.");
            return reserva;
        }

        // Traduce el resultado del SP a la excepción de negocio que muestra la pantalla
        private static void LanzarSiFallo(ResultadoCambioHabitacion resultado, IPoliticaCambioHabitacion_68SA politica)
        {
            switch (resultado)
            {
                case ResultadoCambioHabitacion.Ok: return;
                case ResultadoCambioHabitacion.ReservaNoEnCurso:
                    throw new EstadoReservaInvalidoException_68SA(politica.AdmiteReservaConfirmada
                        ? "Solo se puede reubicar una reserva Confirmada o una estadía En Curso."
                        : "El upgrade solo se puede registrar con la estadía En Curso.");
                case ResultadoCambioHabitacion.HabitacionNoDisponible: throw new HabitacionNoDisponibleException_68SA("La habitación elegida dejó de estar libre hasta el egreso.");
                case ResultadoCambioHabitacion.MismaHabitacion: throw new MismaHabitacionException_68SA();
                case ResultadoCambioHabitacion.SinNochesRestantes: throw new SinNochesRestantesException_68SA();
                case ResultadoCambioHabitacion.TarifaNoSuperior: throw new TarifaNoSuperiorException_68SA();
                case ResultadoCambioHabitacion.CapacidadInsuficiente: throw new CapacidadExcedidaException_68SA();
                default: throw new DatosInvalidosException_68SA("Motivo de cambio de habitación no válido.");
            }
        }

        #endregion
    }
}
