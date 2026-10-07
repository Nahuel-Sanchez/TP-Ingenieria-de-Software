using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_08YS
{
    public interface IDbFactory_08YS
    {
        #region Fábrica

        IDbConnection CreateConnection();
        IDbCommand CreateCommand(string query, IDbConnection connection);
        IDbDataParameter CreateParameter(string name, object value);
        IDbDataParameter CreateOutputParameter(string name);
        IDbDataParameter CreateTableValuedParameter(string name, DataTable value, string typeName);

        #endregion

        #region Consultas

        string GetConnectionString();

        #endregion

        #region Métodos públicos

        // DataAdapter no tiene Fill(DataTable) en IDataAdapter de visual studio 
        // por lo que el factory se va a encargar de llenar el DataTable
        DataTable FillDataTable(IDbCommand command);
        DataSet FillDataSet(IDbCommand command);

        #endregion
    }
}
