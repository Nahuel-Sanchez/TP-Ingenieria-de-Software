using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_08YS.Metricas
{
    public class ResumenHuespedes_68SA
    {
        public int TotalAtendidos { get; set; }
        public int Nuevos { get; set; }
        public int Recurrentes { get; set; }
        public decimal TasaRecurrenciaPorcentaje { get; set; }
        public decimal GastoPromedioPorHuesped { get; set; }
        public List<NacionalidadItem_68SA> PorNacionalidad { get; set; } = new List<NacionalidadItem_68SA>();
    }

    public class NacionalidadItem_68SA
    {
        public string Nacionalidad { get; set; }
        public int Cantidad { get; set; }
    }
}
