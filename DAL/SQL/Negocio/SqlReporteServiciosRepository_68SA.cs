using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio.cargos;
using MPP_08YS;
using System;
using System.Data;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlReporteServiciosRepository_68SA : Connection_08YS, IReporteServiciosRepository_68SA
    {
        public SqlReporteServiciosRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        public ReporteServicios_68SA GetServiciosConsumidos(FiltroServicios_68SA filtro)
        {
            filtro = filtro ?? new FiltroServicios_68SA();

            DataSet ds = GetDataSet("sp_ReporteServiciosConsumidos",
                new[]
                {
                    Param("@Desde", filtro.Desde.HasValue ? (object)filtro.Desde.Value.Date : DBNull.Value),
                    Param("@Hasta", filtro.Hasta.HasValue ? (object)filtro.Hasta.Value.Date : DBNull.Value),
                    Param("@Tipo", filtro.Tipo.HasValue ? (object)(int)filtro.Tipo.Value : DBNull.Value),
                    Param("@Estado", filtro.Estado.HasValue ? (object)(int)filtro.Estado.Value : DBNull.Value),
                    Param("@Huesped", string.IsNullOrWhiteSpace(filtro.Huesped) ? (object)DBNull.Value : filtro.Huesped.Trim())
                },
                storedProcedure: true);

            var reporte = new ReporteServicios_68SA { Filtro = filtro, Generado = DateTime.Now };
            if (ds.Tables.Count > 0)
                reporte.Servicios = ServicioConsumidoMapper_68SA.FromDataTable(ds.Tables[0]);
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0 && ds.Tables[1].Rows[0][0] != DBNull.Value)
                reporte.AlojamientoPeriodo = Convert.ToDecimal(ds.Tables[1].Rows[0][0]);
            if (ds.Tables.Count > 2)
                reporte.Productos = ServicioConsumidoMapper_68SA.ProductosFromDataTable(ds.Tables[2]);
            return reporte;
        }
    }
}
