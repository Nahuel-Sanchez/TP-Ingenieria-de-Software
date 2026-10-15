using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using Service_08YS;
using Service_08YS.Entities.Acceso;
using System.Collections.Generic;

namespace BLL_08YS.Negocio
{
    /// <summary>
    /// Bitácora de cambios de reservas (patrón de la cátedra): consulta con filtros por código, nombre,
    /// operación y fechas. Es de solo lectura: los registros (y la marca Act del vigente) los escribe el
    /// trigger de Reservas en cada alta, modificación, cancelación, check-in, check-out, renovación o
    /// cambio de habitación, y la base rechaza cualquier baja o modificación hecha a mano.
    /// </summary>
    public class BitacoraReservaBLL_68SA
    {
        #region Campos

        private readonly IBitacoraReservaRepository_68SA _repo;

        #endregion

        #region Constructor

        public BitacoraReservaBLL_68SA(IBitacoraReservaRepository_68SA repo)
        {
            _repo = repo;
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

        #endregion
    }
}
