using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL.Connection;
using Salon.Entities.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Salon.ADO.DAL
{
    public class ServiceRepository : ISalonManager<Service>
    {
        private readonly ISqlConnectionFactory _connection;

        public ServiceRepository(ISqlConnectionFactory connection)
        {
            _connection = connection;
        }

        public Service Add(Service service)
        {
            Service newService = new Service
            {
                NameOfService = service.NameOfService,
                Price = service.Price,
            };

            string sqlExpression = $"INSERT INTO Services (NameOfService, Price) " +
                                    $"VALUES (N'{newService.NameOfService}', " +
                                    $"{newService.Price})";
            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            command.ExecuteNonQuery();

            sql.Close();

            return newService;
        }

        public void Delete(int id)
        {
            string sqlExpression = $"DELETE FROM Services WHERE Id={id}";

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            command.ExecuteNonQuery();

            sql.Close();
        }

        public IEnumerable<Service> GetList()
        {
            string sqlExpression = "SELECT * FROM Services";

            List<Service> services = new List<Service>();

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Service service = new Service
                    {
                        Id = reader.GetInt32(0),
                        NameOfService = reader.GetString(1),
                        Price = reader.GetDecimal(2)
                    };

                    services.Add(service);
                }
            }

            sql.Close();

            return services;
        }

        public Service GetSingle(int id)
        {
            string sqlExpression = $"SELECT * FROM Services WHERE Id = {id}";

            Service service = new Service();

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    service = new Service
                    {
                        Id = reader.GetInt32(0),
                        NameOfService = reader.GetString(1),
                        Price = reader.GetDecimal(2)
                    };
                }
            }

            sql.Close();

            return service;
        }

        public Service Update(int id, Service service)
        {
            Service serviceToUpdate = new Service
            {
                Id = id,
                NameOfService = service.NameOfService,
                Price = service.Price
            };

            string sqlExpression = "UPDATE Services " +
                                            $"SET NameOfService = N'{serviceToUpdate.NameOfService}'," +
                                            $"Price = '{serviceToUpdate.Price}'" +
                                            $"WHERE Id={id}";

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            command.ExecuteNonQuery();

            sql.Close();

            return serviceToUpdate;
        }

        public List<int> GetIds()
        {
            string sqlExpression = $"EXEC GetServiceIds;";

            List<int> ids = new List<int>();
            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    ids.Add(id);
                }
            }
            sql.Close();

            return ids;
        }

        public IEnumerable<string> GetPhoneNumbers()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetEmails()
        {
            throw new System.NotImplementedException();
        }
    }
}
