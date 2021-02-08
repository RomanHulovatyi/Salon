using Salon.Abstractions.Interfaces;
using Salon.Entities.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Salon.ADO.DAL
{
    public class ServiceRepository : ISalonManager<Service>
    {
        private readonly SqlConnection _connection;

        public ServiceRepository(SqlConnection connection)
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
                                    $"N'{newService.Price}'";
            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();

            _connection.Close();

            return newService;
        }

        public void Delete(int id)
        {
            string sqlExpression = $"DELETE FROM Services WHERE Id={id}";

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();

            _connection.Close();
        }

        public IEnumerable<Service> GetList()
        {
            string sqlExpression = "SELECT * FROM Services";

            List<Service> services = new List<Service>();

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
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

            _connection.Close();

            return services;
        }

        public Service GetSingle(int id)
        {
            string sqlExpression = $"SELECT * FROM Services WHERE Id = {id}";

            Service service = new Service();

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
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

            _connection.Close();

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

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();

            _connection.Close();

            return serviceToUpdate;
        }

        public List<int> GetIds()
        {
            string sqlExpression = $"EXEC GetServiceIds;";

            List<int> ids = new List<int>();
            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    ids.Add(id);
                }
            }
            _connection.Close();

            return ids;
        }
    }
}
