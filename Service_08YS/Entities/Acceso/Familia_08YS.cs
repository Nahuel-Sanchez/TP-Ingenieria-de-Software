using Service_08YS.Entities.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_08YS.Entities.Acceso
{
    public class Familia_08YS : AccessComponent
    {
        public int FamiliaID { get => ID; set => ID = value; }

        private readonly List<AccessComponent> _hijos = new List<AccessComponent>();
        public IReadOnlyList<AccessComponent> Hijos => _hijos.AsReadOnly();

        public override bool EsCompuesto => true;

        public void Agregar(AccessComponent c) => _hijos.Add(c);
        public void Quitar(AccessComponent c) => _hijos.Remove(c);

        public override HashSet<Permiso_08YS> GetPermisos()
        {
            var resultado = new HashSet<Permiso_08YS>(PermisoComparer.Instance);
            foreach (var hijo in _hijos)
                resultado.UnionWith(hijo.GetPermisos()); // UnionWith ignora duplicados
            return resultado;
        }
    }
}
