using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_08YS
{
    public enum MetodoPago
    {
        Efectivo,
        Tarjeta,
        Transferencia
    }

    public class Factura_68SA
    {
        #region Propiedades

        public int Id { get; set; }
        public int ReservaId { get; set; }
        public decimal Monto { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public DateTime FechaEmision { get; set; }

        #endregion
    }
}