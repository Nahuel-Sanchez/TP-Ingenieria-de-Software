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
    public class SqlPisoRepository_68SA : Connection_08YS, IPisoRepository_68SA
    {
        public SqlPisoRepository_68SA(IDbFactory_08YS factory) : base(factory) { }

        public List<Piso_68SA> GetAll()
        {
            DataTable dt = GetDataTable("SELECT PisoID, Numero, Nombre FROM Pisos ORDER BY Numero");
            return PisoMapper_08YS.FromDataTable(dt);
        }

        public Piso_68SA GetById(int pisoId)
        {
            DataTable dt = GetDataTable(
                "SELECT PisoID, Numero, Nombre FROM Pisos WHERE PisoID = @PisoID",
                new[] { Param("@PisoID", pisoId) });

            return dt.Rows.Count > 0 ? PisoMapper_08YS.FromDataRow(dt.Rows[0]) : null;
        }
    }
}
