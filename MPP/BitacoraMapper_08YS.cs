using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_08YS;

namespace MPP_08YS
{
    public static class BitacoraMapper_08YS
    {
        public static BitacoraEvento_08YS FromDataRow(System.Data.DataRow row)
        {
            return new BitacoraEvento_08YS
            {
                Login = row["Username"].ToString(),
                FechaHora = Convert.ToDateTime(row["FechaHora"]),
                Modulo = (Modulo)Convert.ToInt32(row["Modulo"]),
                Evento = (Evento)Convert.ToInt32(row["Evento"]),
                Criticidad = (Criticidad)Convert.ToInt32(row["Criticidad"])

            };
        }

        public static List<BitacoraEvento_08YS> FromDataTable(System.Data.DataTable dt)
        {
            var eventos = new List<BitacoraEvento_08YS>();
            foreach (System.Data.DataRow row in dt.Rows)
                eventos.Add(FromDataRow(row));
            return eventos;
        }
    }
}
