using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL.Connection;
using Salon.Entities.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Salon.ADO.DAL
{
    public class ServiceRepository : ISalonRepository<ServiceEntity>
    {
        private readonly ISqlConnectionFactory _connection;

        public ServiceRepository(ISqlConnectionFactory connection)
        {
            _connection = connection;
        }

        public ServiceEntity Add(ServiceEntity service)
        {
            ServiceEntity newService = new ServiceEntity
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

        public IEnumerable<ServiceEntity> GetList()
        {
            string sqlExpression = "SELECT * FROM Services";

            List<ServiceEntity> services = new List<ServiceEntity>();

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ServiceEntity service = new ServiceEntity
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

        public ServiceEntity GetSingle(int id)
        {
            string sqlExpression = $"SELECT * FROM Services WHERE Id = {id}";

            ServiceEntity service = new ServiceEntity();

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    service = new ServiceEntity
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

        public ServiceEntity Update(int id, ServiceEntity service)
        {
            ServiceEntity serviceToUpdate = new ServiceEntity
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
