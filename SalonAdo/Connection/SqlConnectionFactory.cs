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
        //private readonly string connection = @"Data Source = ROMAN; Initial Catalog = Salon; Integrated Security = True";
        private readonly string connection = @"Server=tcp:romanhulovatyi.database.windows.net,1433;Initial Catalog=Salon;Persist Security Info=False;User ID=roman;Password=Qwerasdfzxcv1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public SqlConnection CreateSqlConnection()
        {
            SqlConnection sqlConnection = new SqlConnection(connection);
            return sqlConnection;
        }
    }
}
