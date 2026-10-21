using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using Service_08YS;
using Service_08YS.Entities.Acceso;
using Service_08YS.Entities.Bitacora;
using System.Collections.Generic;

namespace BLL_08YS.Negocio
{
    /// <summary>
    /// Bitácora de cambios de reservas (patrón de la cátedra): consulta con filtros por código, nombre,
    /// operación y fechas, historial de una reserva y rollback. Los registros (y la marca Act del vigente)
    /// los escribe el trigger de Reservas en cada alta, modificación, cancelación, check-in, check-out,
    /// renovación o cambio de habitación, y la base rechaza cualquier baja o modificación hecha a mano.
    /// El rollback (solo Administrador y Supervisor) vuelve a poner como activo un registro histórico:
    /// restaura la reserva con esa foto y el trigger lo registra como un registro nuevo "Rollback".
    /// </summary>
    public class BitacoraReservaBLL_68SA
    {
        #region Campos

        private readonly IBitacoraReservaRepository_68SA _repo;
        private readonly BitacoraBLL_08YS _bitacoraBll;

        #endregion

        #region Constructor

        public BitacoraReservaBLL_68SA(IBitacoraReservaRepository_68SA repo, BitacoraBLL_08YS bitacoraBll)
        {
            _repo = repo;
            _bitacoraBll = bitacoraBll;
        }

        #endregion

        #region Consultas

        public List<BitacoraReserva_68SA> Filtrar(BitacoraReservaFiltro_68SA filtro)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerBitacoraReservas);
            filtro = filtro ?? new BitacoraReservaFiltro_68SA();

            if (filtro.FechaDesde.HasValue && filtro.FechaHasta.HasValue && filtro.FechaDesde.Value.Date > filtro.FechaHasta.Value.Date)
                throw new RangoFechasInvalidoException_68SA("La fecha inicial no puede ser posterior a la fecha final.");
            if (filtro.Codigo.HasValue && filtro.Codigo.Value <= 0)
                throw new DatosInvalidosException_68SA("El código de reserva debe ser un número mayor a cero.");

            return _repo.Filtrar(filtro);
        }

        /// <summary>Historial de cambios de una reserva: todos sus registros, del más nuevo al más viejo.</summary>
        public List<BitacoraReserva_68SA> GetHistorial(int reservaId)
            => Filtrar(new BitacoraReservaFiltro_68SA { Codigo = reservaId });

        /// <summary>true si el usuario actual puede hacer rollback (para mostrar u ocultar el botón).</summary>
        public bool PuedeHacerRollback()
            => SessionManager_08YS.Instance.HasPermission(Permisos.RollbackBitacoraReservas);

        #endregion

        #region Operaciones

        /// <summary>
        /// Recupera un registro histórico como ACTIVO: la reserva vuelve a tener los datos de esa foto y la
        /// bitácora suma un registro "Rollback" (el que estaba activo queda como historia).
        /// Los cargos adicionales y los pagos no se modifican.
        /// </summary>
        public void Rollback(BitacoraReserva_68SA registro)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.RollbackBitacoraReservas);
            if (registro == null)
                throw new RollbackReservaException_68SA(ResultadoRollbackReserva.RegistroNoEncontrado);
            if (registro.Act)
                throw new RollbackReservaException_68SA(ResultadoRollbackReserva.YaEsActivo);

            var resultado = _repo.Rollback(registro.Id);
            if (resultado != ResultadoRollbackReserva.Ok)
                throw new RollbackReservaException_68SA(resultado);

            DVManager_08YS.Recalcular();
            _bitacoraBll.RegistrarEvento(Evento.ReservaRollbackAplicado, targetUsername: registro.Reserva.ReservaId.ToString());
        }

        #endregion
    }
}
