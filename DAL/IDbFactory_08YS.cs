using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDbFactory_08YS
    {
        IDbConnection CreateConnection();
        IDbCommand CreateCommand(string query, IDbConnection connection);
        IDbDataParameter CreateParameter(string name, object value);

        // DataAdapter no tiene Fill(DataTable) en IDataAdapter de visual studio general
        // por lo que el factory encargara de llenar el DataTable
        DataTable FillDataTable(IDbCommand command);
    }
}
