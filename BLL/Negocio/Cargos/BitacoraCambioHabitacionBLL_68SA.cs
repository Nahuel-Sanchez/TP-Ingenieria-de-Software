using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio.cargos;
using Service_08YS;
using Service_08YS.Entities.Acceso;
using System.Collections.Generic;

namespace BLL_08YS.Negocio
{
    /// <summary>
    /// Bitácora de cambios de habitación (patrón de la cátedra): consulta con filtros por código, nombre y fechas.
    /// Es de solo lectura: los registros (y la marca Act del vigente) los escriben los triggers de Reservas
    /// y la base rechaza cualquier baja o modificación hecha a mano.
    /// </summary>
    public class BitacoraCambioHabitacionBLL_68SA
    {
        #region Campos

        private readonly IBitacoraCambioHabitacionRepository_68SA _repo;

        #endregion

        #region Constructor

        public BitacoraCambioHabitacionBLL_68SA(IBitacoraCambioHabitacionRepository_68SA repo)
        {
            _repo = repo;
        }

        #endregion

        #region Métodos públicos

        public List<BitacoraCambioHabitacion_68SA> Filtrar(BitacoraCambioHabitacionFiltro_68SA filtro)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.VerBitacoraCambiosHabitacion);
            filtro = filtro ?? new BitacoraCambioHabitacionFiltro_68SA();

            if (filtro.FechaDesde.HasValue && filtro.FechaHasta.HasValue && filtro.FechaDesde.Value.Date > filtro.FechaHasta.Value.Date)
                throw new RangoFechasInvalidoException_68SA("La fecha inicial no puede ser posterior a la fecha final.");
            if (filtro.Codigo.HasValue && filtro.Codigo.Value <= 0)
                throw new DatosInvalidosException_68SA("El código de reserva debe ser un número mayor a cero.");

            return _repo.Filtrar(filtro);
        }

        #endregion
    }
}
