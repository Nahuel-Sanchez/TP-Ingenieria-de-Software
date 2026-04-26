using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MockDbFactory_08YS : IDbFactory_08YS
    {
        public DataTable MockData { get; set; } = new DataTable();

        public IDbConnection CreateConnection() => new MockConnection();
        public IDbCommand CreateCommand(string query, IDbConnection conn) => new MockCommand();
        public IDbDataParameter CreateParameter(string name, object value) => new MockParameter(name, value);

        // En lugar de un adapter real, devuelve los datos mock directamente.
        public DataTable FillDataTable(IDbCommand command) => MockData;
    }
}
