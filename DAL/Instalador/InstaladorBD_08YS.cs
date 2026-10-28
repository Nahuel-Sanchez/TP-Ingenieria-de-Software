using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL_08YS.Instalador
{
    /// <summary>
    /// Respaldo para cuando la aplicación se ejecuta sin haber pasado por el instalador
    /// (por ejemplo desde Visual Studio en una máquina nueva): si la base no existe en la
    /// instancia configurada, la crea con los mismos scripts que usa el instalador, que
    /// quedan en la carpeta "Database" junto al .exe:
    ///   1. Deploy_TP_Ing_Soft_v1.sql  (sistema: usuarios, perfiles, bitácora, DV, backup, idioma)
    ///   2. Update_TP_Ing_Soft_v2.sql  (negocio de la versión actual)
    /// Cada script corre en una transacción y ante cualquier error se detiene (no se ignoran
    /// lotes): la base queda creada completa o el error llega a Program.cs y se informa.
    /// Se invoca una sola vez desde BLLInstalador_08YS.AsegurarBaseDatos().
    /// </summary>
    public static class InstaladorBD_08YS
    {
        #region Campos

        public const string NombreBaseDatos = "TP_Ing_Soft";
        private const string CarpetaScripts = "Database";

        private static readonly string[] Scripts =
        {
            "Deploy_TP_Ing_Soft_v1.sql",
            "Update_TP_Ing_Soft_v2.sql"
        };

        #endregion

        #region Consultas

        public static bool ExisteBaseDatos(string instancia)
        {
            using (var conn = new SqlConnection(ArmarConnectionStringMaster(instancia)))
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM sys.databases WHERE name = @n", conn))
                {
                    cmd.Parameters.AddWithValue("@n", NombreBaseDatos);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        #endregion

        #region Métodos públicos

        public static void InstalarBaseDatos(string instancia)
        {
            string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CarpetaScripts);

            foreach (string nombre in Scripts)
            {
                string ruta = Path.Combine(carpeta, nombre);
                if (!File.Exists(ruta))
                    throw new FileNotFoundException(
                        $"No se encontró el script '{nombre}' en la carpeta '{CarpetaScripts}' junto al ejecutable. " +
                        "Instale el sistema con el instalador de Horizon Hotel & Resort.",
                        ruta);
            }

            using (var conn = new SqlConnection(ArmarConnectionStringMaster(instancia)))
            {
                conn.Open();
                foreach (string nombre in Scripts)
                    EjecutarScript(conn, Path.Combine(carpeta, nombre));
            }
        }

        /// <summary>
        /// Punto de entrada único: si la base no existe en la instancia configurada,
        /// la crea. Se llama desde BLLInstalador_08YS al arrancar la app.
        /// </summary>
        public static void AsegurarBaseDatos()
        {
            string instancia = new SqlConnectionStringBuilder(SqlDbFactory_08YS.ConnectionString).DataSource;

            if (!ExisteBaseDatos(instancia))
                InstalarBaseDatos(instancia);
        }

        #endregion

        #region Métodos auxiliares

        /// <summary>
        /// Ejecuta un script del instalador lote por lote sobre la misma conexión (el script
        /// cambia de base con USE y abre una transacción que abarca varios lotes).
        /// Las directivas de sqlcmd (líneas que empiezan con ":") no las entiende SqlCommand y se quitan.
        /// Si un lote falla, la excepción se propaga: con XACT_ABORT ON el servidor ya deshizo la transacción.
        /// </summary>
        private static void EjecutarScript(SqlConnection conn, string ruta)
        {
            string script = File.ReadAllText(ruta);
            script = Regex.Replace(script, @"^\s*:.*$", string.Empty, RegexOptions.Multiline);

            // SqlCommand no entiende el separador "GO": hay que partir el script en lotes
            string[] lotes = Regex.Split(script, @"^\s*GO\s*$",
                RegexOptions.Multiline | RegexOptions.IgnoreCase);

            foreach (string lote in lotes)
            {
                if (string.IsNullOrWhiteSpace(lote)) continue;

                try
                {
                    using (var cmd = new SqlCommand(lote, conn) { CommandTimeout = 300 })
                        cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    RegistrarError(Path.GetFileName(ruta), ex);
                    throw new InvalidOperationException(
                        $"Falló el script {Path.GetFileName(ruta)}: {ex.Message}", ex);
                }
            }
        }

        private static void RegistrarError(string script, SqlException ex)
        {
            try
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "instalador_db.log");
                File.AppendAllText(logPath,
                    $"[{DateTime.Now}] {script}: {ex.Message}{Environment.NewLine}");
            }
            catch { /* sin permisos de escritura en la carpeta: el error igual llega al usuario */ }
        }

        private static string ArmarConnectionStringMaster(string instancia)
            => new SqlConnectionStringBuilder(SqlDbFactory_08YS.ConnectionString)
            {
                DataSource = instancia,
                InitialCatalog = "master"
            }.ConnectionString;

        #endregion
    }
}
