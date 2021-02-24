using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL.Connection;
using Salon.Entities.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Salon.ADO.DAL
{
    public class OrderRepository : ISalonRepository<OrderEntity>
    {
        private readonly ISqlConnectionFactory _connection;

        public OrderRepository(ISqlConnectionFactory connection)
        {
            _connection = connection;
        }

        public OrderEntity Add(OrderEntity order)
        {
            OrderEntity newOrder = new OrderEntity
            {
                ServiceId = order.ServiceId,
                CustomerId = order.CustomerId,
                DateOfProcedure = order.DateOfProcedure,
                StatusId = order.StatusId
            };

            string sqlExpression = $"INSERT INTO Orders (ServiceId, CustomerId, DateOfProcedure, StatusId) " +
                                    $"VALUES ('{newOrder.ServiceId}', " +
                                    $"'{newOrder.CustomerId}', " +
                                    $"'{newOrder.DateOfProcedure}', " +
                                    $"N'7')";
            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            command.ExecuteNonQuery();

            sql.Close();

            return newOrder;
        }

        public void Delete(int id)
        {
            string sqlExpression = $"DELETE FROM Orders WHERE Id={id}";

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            command.ExecuteNonQuery();

            sql.Close();
        }

        public IEnumerable<OrderEntity> GetList()
        {
            string sqlExpression = "SELECT * FROM Orders";

            List<OrderEntity> orders = new List<OrderEntity>();

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    OrderEntity order = new OrderEntity
                    {
                        Id = reader.GetInt32(0),
                        ServiceId = reader.GetInt32(1),
                        CustomerId = reader.GetInt32(2),
                        DateOfProcedure = reader.GetDateTime(3),
                        StatusId = reader.GetInt32(4)
                    };

                    orders.Add(order);
                }
            }

            sql.Close();

            return orders;
        }

        public OrderEntity GetSingle(int id)
        {
            string sqlExpression = $"SELECT * FROM Orders WHERE Id = {id}";

            OrderEntity order = new OrderEntity();

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    order = new OrderEntity
                    {
                        Id = reader.GetInt32(0),
                        ServiceId = reader.GetInt32(1),
                        CustomerId = reader.GetInt32(2),
                        DateOfProcedure = reader.GetDateTime(3),
                        StatusId = reader.GetInt32(4)
                    };
                }
            }

            sql.Close();

            return order;
        }

        public OrderEntity Update(int id, OrderEntity order)
        {
            OrderEntity orderToUpdate = new OrderEntity
            {
                Id = id,
                ServiceId = order.ServiceId,
                CustomerId = order.CustomerId,
                DateOfProcedure = order.DateOfProcedure,
                StatusId = order.StatusId
            };

            string sqlExpression = "UPDATE Orders " +
                                            $"SET ServiceId = '{orderToUpdate.ServiceId}'," +
                                            $"CustomerId = '{orderToUpdate.CustomerId}'," +
                                            $"DateOfProcedure = '{orderToUpdate.DateOfProcedure}'," +
                                            $"StatusId = '{orderToUpdate.StatusId}' " +
                                            $"WHERE Id={id}";

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            command.ExecuteNonQuery();

            sql.Close();

            return orderToUpdate;
        }

        public OrderViewEntity GetSingleView(int id)
        {
            string sqlExpression = "SELECT * FROM OrderTable WHERE Id = {id}";

            OrderViewEntity order = new OrderViewEntity();

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    order = new OrderViewEntity
                    {
                        Id = reader.GetInt32(0),
                        Customer = reader.GetString(1),
                        Service = reader.GetString(2),
                        Price = reader.GetDecimal(3),
                        Date = reader.GetDateTime(4),
                        Status = reader.GetString(5)
                    };
                }
            }

            sql.Close();

            return order;
        }

        public IEnumerable<OrderViewEntity> GetView()
        {
            string sqlExpression = "SELECT * FROM OrderTable ORDER BY [Date]";

            List<OrderViewEntity> orders = new List<OrderViewEntity>();

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    OrderViewEntity order = new OrderViewEntity
                    {
                        Id = reader.GetInt32(0),
                        Customer = reader.GetString(1),
                        Service = reader.GetString(2),
                        Price = reader.GetDecimal(3),
                        Date = reader.GetDateTime(4),
                        Status = reader.GetString(5)
                    };

                    orders.Add(order);
                }
            }

            sql.Close();

            return orders;
        }

        public List<int> GetIds()
        {
            string sqlExpression = $"EXEC GetOrderIds;";

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
