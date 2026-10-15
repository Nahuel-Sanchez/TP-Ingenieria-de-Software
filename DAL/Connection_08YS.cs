using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS
{
    public abstract class Connection_08YS
    {
        #region Campos

        private readonly IDbFactory_08YS _factory;

        #endregion

        #region Propiedades

        /// <summary>
        /// Usuario del sistema que hace las operaciones (lo configura la BLL con el de la sesión actual).
        /// Se informa a la base en cada escritura para que el trigger de la bitácora de reservas sepa quién fue.
        /// </summary>
        public static Func<string> UsuarioActual { get; set; }

        #endregion

        #region Constructor

        protected Connection_08YS(IDbFactory_08YS factory)
        {
            _factory = factory;
        }

        #endregion

        #region Métodos auxiliares

        // Helper para crear parametros de los repositorios concretos sin necesitar una referencia directa al factory
        protected IDbDataParameter Param(string name, object value)
            => _factory.CreateParameter(name, value);

        protected IDbDataParameter ParamOutput(string name)
            => _factory.CreateOutputParameter(name);

        // Salida DECIMAL(precision, escala): los montos que calcula un SP (ej. el cargo de un upgrade)
        protected IDbDataParameter ParamOutputDecimal(string name, byte precision = 10, byte escala = 2)
        {
            IDbDataParameter parametro = _factory.CreateOutputParameter(name);
            parametro.DbType = DbType.Decimal;
            parametro.Precision = precision;
            parametro.Scale = escala;
            return parametro;
        }

        /// <summary>
        /// Deja en el contexto de la sesión de SQL Server el usuario del sistema y la operación, que lee el trigger
        /// de la bitácora de reservas. Se hace en la misma conexión que el comando (al volver al pool se borra).
        /// </summary>
        private void InformarContextoBitacora(IDbConnection conn, string operacionReserva)
        {
            string usuario = UsuarioActual?.Invoke();
            // Procesos automáticos sin sesión (por ejemplo, los no-show al iniciar): quedan a nombre del sistema
            if (string.IsNullOrEmpty(usuario) && operacionReserva != null)
                usuario = "Sistema";

            using (IDbCommand contexto = _factory.CreateCommand(
                "EXEC sys.sp_set_session_context @key = N'UsuarioApp', @value = @UsuarioApp; " +
                "EXEC sys.sp_set_session_context @key = N'OperacionReserva', @value = @OperacionReserva;", conn))
            {
                contexto.Parameters.Add(_factory.CreateParameter("@UsuarioApp", (object)usuario ?? DBNull.Value));
                contexto.Parameters.Add(_factory.CreateParameter("@OperacionReserva", (object)operacionReserva ?? DBNull.Value));
                contexto.ExecuteNonQuery();
            }
        }

        protected IDbDataParameter ParamTVP(string name, DataTable value, string typeName = "dbo.IntList")
            => _factory.CreateTableValuedParameter(name, value, typeName);

        protected DataTable GetDataTable(string query, IDbDataParameter[] parameters = null, bool storedProcedure = false)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            using (IDbCommand cmd = _factory.CreateCommand(query, conn))
            {
                if (storedProcedure)
                    cmd.CommandType = CommandType.StoredProcedure;
                
                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);

                return _factory.FillDataTable(cmd);
            }
        }

        protected DataSet GetDataSet(string query, IDbDataParameter[] parameters = null, bool storedProcedure = false)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            using (IDbCommand cmd = _factory.CreateCommand(query, conn))
            {
                if (storedProcedure) 
                    cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    foreach (var p in parameters) cmd.Parameters.Add(p);

                return _factory.FillDataSet(cmd);
            }
        }

        /// <param name="operacionReserva">
        /// Operación que se registra en la bitácora de reservas si el comando modifica una reserva
        /// (por ejemplo "No-show"). Si es null, el trigger la deduce de lo que cambió.
        /// </param>
        protected bool ExecuteNonQuery(string query, IDbDataParameter[] parameters = null, bool storedProcedure = false,
            string operacionReserva = null)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            using (IDbCommand cmd = _factory.CreateCommand(query, conn))
            {
                if (storedProcedure)
                    cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);

                conn.Open();
                InformarContextoBitacora(conn, operacionReserva);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        protected T ExecuteScalar<T>(string query, IDbDataParameter[] parameters = null, bool storedProcedure = false)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            using (IDbCommand cmd = _factory.CreateCommand(query, conn))
            {
                if (storedProcedure)
                    cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);

                conn.Open();
                InformarContextoBitacora(conn, null);
                object result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    return default(T);

                return (T)Convert.ChangeType(result, typeof(T));
            }
        }

        #endregion
    }
}
