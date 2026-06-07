using Service_08YS;
using Service_08YS.Entities.Acceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_08YS
{
    internal static class PermissionFilter_08YS
    {
        public static void Aplicar(Control raiz, Dictionary<string, Permisos> mapa)
        {
            foreach (var kvp in mapa)
            {
                var encontrados = raiz.Controls.Find(kvp.Key, searchAllChildren: true);
                if (encontrados.Length > 0)
                    encontrados[0].Visible = SessionManager_08YS.Instance.HasPermission(kvp.Value);
            }
        }
    }
}
