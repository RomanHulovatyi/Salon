using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.ADO.DAL.Connection
{
    public interface ISqlConnectionFactory
    {
        public SqlConnection CreateSqlConnection();
    }
}
