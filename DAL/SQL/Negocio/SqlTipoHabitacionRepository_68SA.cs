using BE_08YS;
using DAL_08YS.Interfaces_Repositories.Negocio.habitacion;
using MPP_08YS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.SQL.Negocio
{
    public class SqlTipoHabitacionRepository_68SA : Connection_08YS, ITipoHabitacionRepository_68SA
    {
        private const string BaseSelect =
            "SELECT TipoHabitacionID, Nombre, Descripcion, Capacidad, TarifaNoche FROM TiposHabitacion";

        public SqlTipoHabitacionRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        public List<TipoHabitacion> GetAll()
        {
            DataTable dt = GetDataTable(BaseSelect + " ORDER BY Nombre");
            return TipoHabitacionMapper_08YS.FromDataTable(dt);
        }

        public TipoHabitacion GetById(int tipoHabitacionId)
        {
            DataTable dt = GetDataTable(
                BaseSelect + " WHERE TipoHabitacionID = @Id",
                new[] { Param("@Id", tipoHabitacionId) });

            return dt.Rows.Count > 0 ? TipoHabitacionMapper_08YS.FromDataRow(dt.Rows[0]) : null;
        }
    }
}
