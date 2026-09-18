using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_08YS.Metricas
{
    public class ResumenOcupacion_68SA
    {
        public int Total { get; set; }
        public int Disponibles { get; set; }
        public int Reservadas { get; set; }
        public int Ocupadas { get; set; }
        public int EnLimpieza { get; set; }
        public int FueraDeServicio { get; set; }
        public decimal TasaOcupacionPorcentaje { get; set; }
        public double DuracionPromedioEstadiaHoras { get; set; }
    }
}
