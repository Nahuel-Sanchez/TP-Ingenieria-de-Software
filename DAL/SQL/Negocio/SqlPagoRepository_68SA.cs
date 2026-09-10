using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio;
using MPP_08YS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlPagoRepository_68SA : Connection_08YS, IPagoRepository_68SA
    {
        public SqlPagoRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        public int Registrar(Pago_68SA pago)
        {
            return ExecuteScalar<int>(
                @"INSERT INTO Pagos (ReservaID, Monto, MetodoPago, FechaPago)
                  VALUES (@ReservaID, @Monto, @MetodoPago, @FechaPago);
                  SELECT CAST(SCOPE_IDENTITY() AS int);",
                new[]
                {
                    Param("@ReservaID",  pago.ReservaId),
                    Param("@Monto",      pago.Monto),
                    Param("@MetodoPago", (int)pago.MetodoPago),
                    Param("@FechaPago",  pago.FechaPago)
                });
        }

        public List<Pago_68SA> GetByReserva(int reservaId)
        {
            DataTable dt = GetDataTable(
                "SELECT PagoID, ReservaID, Monto, MetodoPago, FechaPago FROM Pagos WHERE ReservaID = @ReservaID ORDER BY FechaPago",
                new[] { Param("@ReservaID", reservaId) });

            return PagoMapper_68SA.FromDataTable(dt);
        }

        public decimal GetTotalPagado(int reservaId)
        {
            return ExecuteScalar<decimal>(
                "SELECT ISNULL(SUM(Monto), 0) FROM Pagos WHERE ReservaID = @ReservaID",
                new[] { Param("@ReservaID", reservaId) });
        }
    }
}
