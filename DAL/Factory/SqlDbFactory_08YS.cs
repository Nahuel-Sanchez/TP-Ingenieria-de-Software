using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS
{
    public class SqlDbFactory_08YS : IDbFactory_08YS
    {
        private readonly string _connectionString;

        public SqlDbFactory_08YS()
        {
            _connectionString = "Data Source=DESKTOP-CRINK3R\\SQLEXPRESS;Initial Catalog=TP_Ing_Soft;Integrated Security=True;";
            //_connectionString = "Data Source=desktop-gciu8b0;Initial Catalog=TP_Ing_Soft;Integrated Security=True";
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        public IDbCommand CreateCommand(string query, IDbConnection connection)
            => new SqlCommand(query, (SqlConnection)connection);

        public IDbDataParameter CreateParameter(string name, object value)
            => new SqlParameter(name, value ?? DBNull.Value);

        public IDbDataParameter CreateOutputParameter(string name)
       => new SqlParameter(name, SqlDbType.Int) { Direction = ParameterDirection.Output };

        public IDbDataParameter CreateTableValuedParameter(string name, DataTable value, string typeName)
            => new SqlParameter(name, SqlDbType.Structured) { TypeName = typeName, Value = value };

        public DataTable FillDataTable(IDbCommand command)
        {
            var dt = new DataTable();
            using (var adapter = new SqlDataAdapter((SqlCommand)command))
                adapter.Fill(dt);
            return dt;
        }
        public DataSet FillDataSet(IDbCommand command)
        {
            var ds = new DataSet();
            using (var adapter = new SqlDataAdapter((SqlCommand)command))
                adapter.Fill(ds);
            return ds;
        }
    }
}
