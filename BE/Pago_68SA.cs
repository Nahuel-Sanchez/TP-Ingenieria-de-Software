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

    public class Pago_68SA
    {
        public int Id { get; set; }
        public int ReservaId { get; set; }
        public decimal Monto { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public DateTime FechaPago { get; set; }
    }
}