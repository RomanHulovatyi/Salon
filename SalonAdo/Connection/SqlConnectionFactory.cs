using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.ADO.DAL.Connection
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string connection = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True";

        public SqlConnection CreateSqlConnection()
        {
            SqlConnection sqlConnection = new SqlConnection(connection);
            return sqlConnection;
        }
    }
}
