using Service_08YS.Entities.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_08YS.Entities.Acceso
{
    public class Permiso : AccessComponent
    {
        public int PermisoID { get => ID; set => ID = value; }
        public string Descripcion { get; set; }

        public override bool EsCompuesto => false;

        // El leaf devuelve un HashSet con sí mismo — ya usa el comparer correcto
        public override HashSet<Permiso> GetPermisos()
            => new HashSet<Permiso>(new[] { this }, PermisoComparer.Instance);
    }
}
