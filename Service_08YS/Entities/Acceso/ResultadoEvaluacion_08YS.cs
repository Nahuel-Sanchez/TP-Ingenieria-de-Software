using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_08YS.Entities.Acceso
{
    public class ResultadoEvaluacion_08YS
    {
        public enum Tipo { Valido, ConflictoIrresoluble, SugerenciaReemplazo }

        public Tipo Resultado { get; set; }
        public List<AccessComponent> ComponentesAReemplazar { get; set; } = new List<AccessComponent>();
        public string Mensaje { get; set; }
    }
}
