using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_08YS.Metricas
{
    public class ResumenIngresos_68SA
    {
        public decimal IngresosPeriodo { get; set; }
        public decimal TicketPromedio { get; set; }
        public decimal PorcentajeReservasAnuladas { get; set; }
        public decimal MontoAnuladoPeriodo { get; set; }
        public List<OrigenGastoItem_68SA> OrigenDelGasto { get; set; } = new List<OrigenGastoItem_68SA>();
    }

    public class OrigenGastoItem_68SA
    {
        public string Origen { get; set; }
        public decimal Monto { get; set; }
    }
}
