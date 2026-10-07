using BE_08YS;
using Service_08YS.Entities.Acceso;
using Service_08YS.Entities.Bitacora;
using System;

namespace BLL_08YS.Negocio
{
    /// <summary>
    /// Regla de negocio de cada tipo de cambio de habitación (patrón Strategy).
    /// CambioHabitacionBLL_68SA no pregunta "¿es upgrade?": delega en la política del motivo.
    /// Sumar un motivo nuevo (por ejemplo, un upgrade de cortesía) es agregar una clase, sin tocar el servicio.
    /// </summary>
    public interface IPoliticaCambioHabitacion_68SA
    {
        #region Propiedades

        MotivoCambioHabitacion Motivo { get; }
        Permisos PermisoRequerido { get; }
        Evento EventoBitacora { get; }

        /// <summary>true: solo se ofrecen habitaciones de tarifa mayor a la vigente.</summary>
        bool SoloCategoriaSuperior { get; }

        /// <summary>true: también se aplica a una reserva Confirmada sin check-in (se cambia la habitación de toda la estadía).</summary>
        bool AdmiteReservaConfirmada { get; }

        #endregion

        #region Validaciones

        /// <summary>Datos propios del motivo (por ejemplo, la observación obligatoria de una reubicación).</summary>
        void ValidarSolicitud(SolicitudCambioHabitacion_68SA solicitud);

        #endregion

        #region Métodos públicos

        /// <summary>Calcula el cargo; lanza la excepción de negocio si el cambio no corresponde.</summary>
        CotizacionCambioHabitacion_68SA Cotizar(decimal tarifaVigente, decimal tarifaNueva, int nochesRestantes);

        /// <summary>Texto del cargo que queda en la cuenta del huésped; null si el cambio no se cobra.</summary>
        string DescribirCargo(Habitacion_68SA habitacionNueva, CotizacionCambioHabitacion_68SA cotizacion);

        #endregion
    }

    /// <summary>
    /// Upgrade pedido por el huésped: solo a una tarifa mayor y con al menos una noche por delante.
    /// Cargo = noches restantes x tarifa nueva - lo ya pagado por esas noches = (nueva - vigente) x noches.
    /// </summary>
    public sealed class PoliticaUpgrade_68SA : IPoliticaCambioHabitacion_68SA
    {
        #region Propiedades

        public MotivoCambioHabitacion Motivo => MotivoCambioHabitacion.Upgrade;
        public Permisos PermisoRequerido => Permisos.RegistrarUpgradeHabitacion;
        public Evento EventoBitacora => Evento.HabitacionUpgradeRegistrado;
        public bool SoloCategoriaSuperior => true;
        public bool AdmiteReservaConfirmada => false; // el upgrade lo pide un huésped ya alojado

        #endregion

        #region Validaciones

        public void ValidarSolicitud(SolicitudCambioHabitacion_68SA solicitud)
        {
            if (solicitud.Observacion != null && solicitud.Observacion.Trim().Length > 200)
                throw new DatosInvalidosException_68SA("La observación no puede superar los 200 caracteres.");
        }

        #endregion

        #region Métodos públicos

        public CotizacionCambioHabitacion_68SA Cotizar(decimal tarifaVigente, decimal tarifaNueva, int nochesRestantes)
        {
            if (tarifaNueva <= tarifaVigente)
                throw new TarifaNoSuperiorException_68SA();
            if (nochesRestantes <= 0)
                throw new SinNochesRestantesException_68SA();

            return new CotizacionCambioHabitacion_68SA
            {
                Motivo = Motivo,
                TarifaVigente = tarifaVigente,
                TarifaNueva = tarifaNueva,
                NochesRestantes = nochesRestantes,
                Monto = (tarifaNueva - tarifaVigente) * nochesRestantes
            };
        }

        public string DescribirCargo(Habitacion_68SA habitacionNueva, CotizacionCambioHabitacion_68SA cotizacion)
        {
            string noches = cotizacion.NochesRestantes == 1 ? "1 noche" : $"{cotizacion.NochesRestantes} noches";
            string texto = $"Upgrade a hab. {habitacionNueva.NroHabitacion} ({habitacionNueva.Tipo?.Nombre}): "
                         + $"{noches} x ${cotizacion.DiferenciaPorNoche:N2} de diferencia";
            return texto.Length <= 200 ? texto : texto.Substring(0, 199) + "…";
        }

        #endregion
    }

    /// <summary>
    /// Reubicación por motivos del hotel (mantenimiento, problema físico, decisión operativa):
    /// a cualquier habitación libre con capacidad, sin cargo y sin cambiar la tarifa vigente. El motivo es obligatorio.
    /// Sirve para una estadía en curso y también para una reserva confirmada que todavía no hizo el check-in.
    /// </summary>
    public sealed class PoliticaReubicacionAdministrativa_68SA : IPoliticaCambioHabitacion_68SA
    {
        #region Campos

        public const int LargoMinimoObservacion = 5;

        #endregion

        #region Propiedades

        public MotivoCambioHabitacion Motivo => MotivoCambioHabitacion.Administrativo;
        public Permisos PermisoRequerido => Permisos.ReubicarHuesped;
        public Evento EventoBitacora => Evento.HuespedReubicado;
        public bool SoloCategoriaSuperior => false;
        public bool AdmiteReservaConfirmada => true; // ej.: su habitación pasa a mantenimiento antes del ingreso

        #endregion

        #region Validaciones

        public void ValidarSolicitud(SolicitudCambioHabitacion_68SA solicitud)
        {
            string observacion = solicitud.Observacion?.Trim();
            if (string.IsNullOrEmpty(observacion) || observacion.Length < LargoMinimoObservacion)
                throw new DatosInvalidosException_68SA("Indicá el motivo de la reubicación (por ejemplo: \"Pérdida de agua en el baño\").");
            if (observacion.Length > 200)
                throw new DatosInvalidosException_68SA("El motivo no puede superar los 200 caracteres.");
            solicitud.Observacion = observacion;
        }

        #endregion

        #region Métodos públicos

        public CotizacionCambioHabitacion_68SA Cotizar(decimal tarifaVigente, decimal tarifaNueva, int nochesRestantes)
        {
            return new CotizacionCambioHabitacion_68SA
            {
                Motivo = Motivo,
                TarifaVigente = tarifaVigente,
                TarifaNueva = tarifaNueva,
                NochesRestantes = Math.Max(0, nochesRestantes),
                Monto = 0m
            };
        }

        public string DescribirCargo(Habitacion_68SA habitacionNueva, CotizacionCambioHabitacion_68SA cotizacion) => null;

        #endregion
    }
}
