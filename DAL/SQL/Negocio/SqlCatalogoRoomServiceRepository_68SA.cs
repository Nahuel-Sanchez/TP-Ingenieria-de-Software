using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio.cargos;
using MPP_08YS;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlCatalogoRoomServiceRepository_68SA : Connection_08YS, ICatalogoRoomServiceRepository_68SA
    {
        #region Campos

        private const string Select = "SELECT ItemID, Nombre, Descripcion, PrecioUnitario, Activo FROM ItemsRoomService";

        #endregion

        #region Constructor

        public SqlCatalogoRoomServiceRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        #endregion

        #region Consultas

        public List<ItemRoomService_68SA> GetItems(bool incluirInactivos)
        {
            string query = Select + (incluirInactivos ? "" : " WHERE Activo = 1") + " ORDER BY Activo DESC, Nombre";
            return ItemRoomServiceMapper_68SA.FromDataTable(GetDataTable(query));
        }

        public ItemRoomService_68SA GetById(int itemId)
        {
            DataTable dt = GetDataTable(Select + " WHERE ItemID = @ItemID", new[] { Param("@ItemID", itemId) });
            return dt.Rows.Count > 0 ? ItemRoomServiceMapper_68SA.FromDataRow(dt.Rows[0]) : null;
        }

        public bool ExisteNombre(string nombre, int? excluirItemId = null)
        {
            return ExecuteScalar<int>(@"
                SELECT COUNT(1) FROM ItemsRoomService
                WHERE Nombre = @Nombre AND (@ExcluirID IS NULL OR ItemID <> @ExcluirID)",
                new[] { Param("@Nombre", nombre), Param("@ExcluirID", (object)excluirItemId ?? DBNull.Value) }) > 0;
        }

        #endregion

        #region Operaciones

        public int Crear(ItemRoomService_68SA item)
        {
            return ExecuteScalar<int>(@"
                INSERT INTO ItemsRoomService (Nombre, Descripcion, PrecioUnitario, Activo)
                VALUES (@Nombre, @Descripcion, @PrecioUnitario, @Activo);
                SELECT CAST(SCOPE_IDENTITY() AS int);",
                Parametros(item));
        }

        public void Modificar(ItemRoomService_68SA item)
        {
            var parametros = new List<IDbDataParameter>(Parametros(item)) { Param("@ItemID", item.Id) };
            ExecuteNonQuery(@"
                UPDATE ItemsRoomService
                SET Nombre = @Nombre, Descripcion = @Descripcion, PrecioUnitario = @PrecioUnitario, Activo = @Activo
                WHERE ItemID = @ItemID",
                parametros.ToArray());
        }

        #endregion

        #region Métodos auxiliares

        private IDbDataParameter[] Parametros(ItemRoomService_68SA item) => new[]
        {
            Param("@Nombre", item.Nombre),
            Param("@Descripcion", (object)item.Descripcion ?? DBNull.Value),
            Param("@PrecioUnitario", item.PrecioUnitario),
            Param("@Activo", item.Activo)
        };

        #endregion
    }
}
