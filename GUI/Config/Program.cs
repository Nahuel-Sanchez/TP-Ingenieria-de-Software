using BLL_08YS;
using GUI;
using Service_08YS.Entities.Bitacora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_08YS
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EventCatalog_08YS.CatalogValidation();
            DVManager_08YS.Inicializar(BLLFactory_08YS.CreateDvBLL());
            Application.Run(new FormLogin_08YS());
        }
    }
}
