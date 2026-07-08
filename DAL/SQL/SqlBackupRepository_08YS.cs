using DAL_08YS.Interfaces_Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS.SQL
{
    public class SqlBackupRepository_08YS : Connection_08YS, IBackupRepository_08YS
    {
        private readonly IDbFactory_08YS _factory;

        public SqlBackupRepository_08YS(IDbFactory_08YS factory) : base(factory)
        {
            _factory = factory;
        }

        // Usa la infraestructura normal de Connection_08YS
        public void RealizarBackup(string rutaArchivo, string nombreBaseDatos)
            => ExecuteNonQuery("sp_EjecutarBackup",
                new[]
                {
                Param("@RutaArchivo",     rutaArchivo),
                Param("@NombreBaseDatos", nombreBaseDatos)
                },
                storedProcedure: true);

        /// <summary>
        /// Valida que SQL Server pueda escribir en la carpeta ejecutando un backup de prueba.
        /// Es la única forma confiable de verificar permisos del servicio de SQL Server,
        /// ya que el proceso que escribe es el servicio (NT SERVICE\MSSQL$SQLEXPRESS),
        /// no la app C#. El archivo de prueba se elimina inmediatamente.
        /// </summary>
        public void ValidarPermisoEscritura(string carpeta, string nombreBaseDatos)
        {
            string archivoTest = Path.Combine(carpeta, "_horizonhotel_permtest.bak");
            try
            {
                ExecuteNonQuery(
                    $"BACKUP DATABASE [{nombreBaseDatos}] TO DISK = @f " +
                    $"WITH FORMAT, INIT, NAME = 'test', SKIP, NOREWIND, NOUNLOAD, STATS = 100",
                    new[] { Param("@f", archivoTest) });
            }
            finally
            {
                // Eliminar siempre el archivo de prueba, haya fallado o no
                try { if (File.Exists(archivoTest)) File.Delete(archivoTest); }
                catch { }
            }
        }

        public void RestoreBackup(string rutaBak, string nombreBaseDatos)
        {
            string masterCs = BuildMasterConnectionString();

            using (var conn = new SqlConnection(masterCs))
            {
                conn.Open();
                EjecutarEnMaster(conn,
                    $"ALTER DATABASE [{nombreBaseDatos}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                EjecutarEnMaster(conn,
                    $"RESTORE DATABASE [{nombreBaseDatos}] FROM DISK = N'{rutaBak}' WITH REPLACE");
                EjecutarEnMaster(conn,
                    $"ALTER DATABASE [{nombreBaseDatos}] SET MULTI_USER WITH ROLLBACK IMMEDIATE");
            }

            SqlConnection.ClearAllPools();
        }

        private string BuildMasterConnectionString()
            => new SqlConnectionStringBuilder(_factory.GetConnectionString())
            { InitialCatalog = "master" }.ConnectionString;

        private static void EjecutarEnMaster(SqlConnection conn, string query)
        {
            using (var cmd = new SqlCommand(query, conn) { CommandTimeout = 600 })
                cmd.ExecuteNonQuery();
        }
    }
}
