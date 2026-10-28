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
    public class SqlFacturaRepository_68SA : Connection_08YS, IFacturaRepository_68SA
    {
        #region Constructor

        public SqlFacturaRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        #endregion

        #region Consultas

        public List<Factura_68SA> GetByReserva(int reservaId)
        {
            DataTable dt = GetDataTable(
                "SELECT FacturaID, ReservaID, Monto, MetodoPago, FechaEmision FROM Facturas WHERE ReservaID = @ReservaID ORDER BY FechaEmision",
                new[] { Param("@ReservaID", reservaId) });

            return FacturaMapper_68SA.FromDataTable(dt);
        }

        public decimal GetTotalPagado(int reservaId)
        {
            return ExecuteScalar<decimal>(
                "SELECT ISNULL(SUM(Monto), 0) FROM Facturas WHERE ReservaID = @ReservaID",
                new[] { Param("@ReservaID", reservaId) });
        }

        #endregion

        #region Operaciones

        public int Registrar(Factura_68SA factura)
        {
            return ExecuteScalar<int>(
                @"INSERT INTO Facturas (ReservaID, Monto, MetodoPago, FechaEmision)
                  VALUES (@ReservaID, @Monto, @MetodoPago, @FechaEmision);
                  SELECT CAST(SCOPE_IDENTITY() AS int);",
                new[]
                {
                    Param("@ReservaID",  factura.ReservaId),
                    Param("@Monto",      factura.Monto),
                    Param("@MetodoPago", (int)factura.MetodoPago),
                    Param("@FechaEmision",  factura.FechaEmision)
                });
        }

        #endregion
    }
}
