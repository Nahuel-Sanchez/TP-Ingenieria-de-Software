using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using MPP_08YS;
using System;
using System.Data;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlAnalisisDemandaRepository_68SA : Connection_08YS, IAnalisisDemandaRepository_68SA
    {
        public SqlAnalisisDemandaRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        public DatosDemanda_68SA GetDatos(DateTime desdeHistorial, DateTime hasta)
        {
            DataSet ds = GetDataSet("sp_AnalisisDemanda",
                new[]
                {
                    Param("@DesdeHistorial", desdeHistorial.Date),
                    Param("@Hasta", hasta.Date)
                },
                storedProcedure: true);

            var datos = new DatosDemanda_68SA();
            if (ds.Tables.Count > 0) datos.Tipos = AnalisisDemandaMapper_68SA.TiposFromDataTable(ds.Tables[0]);
            if (ds.Tables.Count > 1) datos.Reservas = AnalisisDemandaMapper_68SA.ReservasFromDataTable(ds.Tables[1]);
            if (ds.Tables.Count > 2) datos.Cargos = AnalisisDemandaMapper_68SA.CargosFromDataTable(ds.Tables[2]);
            if (ds.Tables.Count > 3) datos.Productos = AnalisisDemandaMapper_68SA.ProductosFromDataTable(ds.Tables[3]);
            return datos;
        }
    }
}
