using Service_08YS.Entities.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_08YS.Entities.Acceso
{
    public class Rol_08YS
    {
        #region Campos

        private readonly List<AccessComponent_08YS> _componentes = new List<AccessComponent_08YS>();

        #endregion

        #region Propiedades

        public int RolID { get; set; }
        public string Nombre { get; set; }
        public IReadOnlyList<AccessComponent_08YS> Componentes => _componentes.AsReadOnly();

        #endregion

        #region Consultas

        public HashSet<Permiso_08YS> GetPermisos()
        {
            var resultado = new HashSet<Permiso_08YS>(PermisoComparer.Instance);
            foreach (var c in _componentes)
                resultado.UnionWith(c.GetPermisos());
            return resultado;
        }

        #endregion

        #region Operaciones

        public void Agregar(AccessComponent_08YS c) => _componentes.Add(c);

        #endregion

        #region Sobrescrituras

        public override string ToString()
        {
            return $"{Nombre}";
        }

        #endregion
    }
}
