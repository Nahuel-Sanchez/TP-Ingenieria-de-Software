using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_08YS.Entities.Acceso
{
    public class ResultadoEvaluacion_08YS
    {
        #region Propiedades

        public Tipo Resultado { get; set; }
        public List<AccessComponent_08YS> ComponentesAReemplazar { get; set; } = new List<AccessComponent_08YS>();
        public string Mensaje { get; set; }

        #endregion

        #region Tipos anidados

        public enum Tipo { Valido, ConflictoIrresoluble, SugerenciaReemplazo }

        #endregion
    }
}
