using BE_08YS;

namespace DAL_08YS.Interfaces_Repositories.Negocio.cargos
{
    /// <summary>Lectura de los servicios consumidos por los huéspedes (reporte del PN2).</summary>
    public interface IReporteServiciosRepository_68SA
    {
        /// <summary>Servicios, alojamiento facturado del período y productos más pedidos según el filtro.</summary>
        ReporteServicios_68SA GetServiciosConsumidos(FiltroServicios_68SA filtro);
    }
}
