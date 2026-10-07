using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio.cargos;
using Service_08YS;
using Service_08YS.Entities.Acceso;
using System;

namespace BLL_08YS.Negocio
{
    /// <summary>
    /// Reporte de servicios consumidos por huésped (RFN2): room service y upgrades de cada estadía,
    /// agrupados por huésped con su total acumulado y los indicadores del período.
    /// </summary>
    public class ReporteServiciosBLL_68SA
    {
        #region Campos

        private readonly IReporteServiciosRepository_68SA _repo;

        #endregion

        #region Constructor

        public ReporteServiciosBLL_68SA(IReporteServiciosRepository_68SA repo)
        {
            _repo = repo;
        }

        #endregion

        #region Operaciones

        public ReporteServicios_68SA Generar(FiltroServicios_68SA filtro)
        {
            SessionManager_08YS.Instance.ValidatePermission(Permisos.GenerarReporteServicios);

            filtro = filtro ?? new FiltroServicios_68SA();
            if (filtro.Desde.HasValue && filtro.Hasta.HasValue && filtro.Desde.Value.Date > filtro.Hasta.Value.Date)
                throw new RangoFechasInvalidoException_68SA("La fecha desde no puede ser posterior a la fecha hasta.");
            if (filtro.Huesped != null && filtro.Huesped.Trim().Length > 100)
                throw new DatosInvalidosException_68SA("La búsqueda de huésped admite hasta 100 caracteres.");

            return _repo.GetServiciosConsumidos(filtro);
        }

        #endregion

        #region Métodos públicos

        public bool PuedeGenerar() => SessionManager_08YS.Instance.HasPermission(Permisos.GenerarReporteServicios);

        #endregion
    }
}
