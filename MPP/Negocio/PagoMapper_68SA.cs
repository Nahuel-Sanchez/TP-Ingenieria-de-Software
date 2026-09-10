using BE_08YS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_08YS
{
    public static class PagoMapper_68SA
    {
        public static List<Pago_68SA> FromDataTable(DataTable dt)
        {
            var pagos = new List<Pago_68SA>();
            foreach (DataRow row in dt.Rows)
                pagos.Add(FromDataRow(row));
            return pagos;
        }

        public static Pago_68SA FromDataRow(DataRow row)
        {
            return new Pago_68SA
            {
                Id = Convert.ToInt32(row["PagoID"]),
                ReservaId = Convert.ToInt32(row["ReservaID"]),
                Monto = Convert.ToDecimal(row["Monto"]),
                MetodoPago = (MetodoPago)Convert.ToInt32(row["MetodoPago"]),
                FechaPago = Convert.ToDateTime(row["FechaPago"])
            };
        }
    }
}
