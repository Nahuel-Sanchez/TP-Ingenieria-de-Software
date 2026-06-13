using Service_08YS.Entities.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_08YS.Entities.Acceso
{
    public class Familia_08YS : AccessComponent_08YS
    {
        public int FamiliaID { get => ID; set => ID = value; }

        private readonly List<AccessComponent_08YS> _hijos = new List<AccessComponent_08YS>();
        public IReadOnlyList<AccessComponent_08YS> Hijos => _hijos.AsReadOnly();

        public void Agregar(AccessComponent_08YS c) => _hijos.Add(c);
        public void Quitar(AccessComponent_08YS c) => _hijos.Remove(c);

        public override HashSet<Permiso_08YS> GetPermisos()
        {
            var resultado = new HashSet<Permiso_08YS>(PermisoComparer.Instance);
            foreach (var hijo in _hijos)
                resultado.UnionWith(hijo.GetPermisos()); // UnionWith ignora duplicados
            return resultado;
        }
    }
}
