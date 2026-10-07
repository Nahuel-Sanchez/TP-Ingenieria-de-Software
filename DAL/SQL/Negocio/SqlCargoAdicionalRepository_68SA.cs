using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio.cargos;
using MPP_08YS;
using System.Collections.Generic;
using System.Data;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlCargoAdicionalRepository_68SA : Connection_08YS, ICargoAdicionalRepository_68SA
    {
        #region Constructor

        public SqlCargoAdicionalRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        #endregion

        #region Consultas

        public List<CargoAdicional_68SA> GetByReserva(int reservaId)
        {
            DataTable dt = GetDataTable(@"
                SELECT c.CargoID, c.ReservaID, c.Tipo, c.Descripcion, c.FechaHora, c.Monto, c.UsuarioRegistroDNI,
                       u.Nombre AS NombreUsuario, u.Apellido AS ApellidoUsuario
                FROM CargosAdicionales c
                LEFT JOIN Users u ON u.DNI = c.UsuarioRegistroDNI
                WHERE c.ReservaID = @ReservaID
                ORDER BY c.FechaHora, c.CargoID",
                new[] { Param("@ReservaID", reservaId) });

            return CargoAdicionalMapper_68SA.FromDataTable(dt);
        }

        public decimal GetTotalByReserva(int reservaId)
        {
            return ExecuteScalar<decimal>(
                "SELECT ISNULL(SUM(Monto), 0) FROM CargosAdicionales WHERE ReservaID = @ReservaID",
                new[] { Param("@ReservaID", reservaId) });
        }

        #endregion
    }
}
