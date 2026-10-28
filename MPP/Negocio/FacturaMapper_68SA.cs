using BE_08YS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_08YS
{
    public static class FacturaMapper_68SA
    {
        public static List<Factura_68SA> FromDataTable(DataTable dt)
        {
            var facturas = new List<Factura_68SA>();
            foreach (DataRow row in dt.Rows)
                facturas.Add(FromDataRow(row));
            return facturas;
        }

        public static Factura_68SA FromDataRow(DataRow row)
        {
            return new Factura_68SA
            {
                Id = Convert.ToInt32(row["FacturaID"]),
                ReservaId = Convert.ToInt32(row["ReservaID"]),
                Monto = Convert.ToDecimal(row["Monto"]),
                MetodoPago = (MetodoPago)Convert.ToInt32(row["MetodoPago"]),
                FechaEmision = Convert.ToDateTime(row["FechaEmision"])
            };
        }
    }
}
