using Salon.Abstractions.Interfaces;
using Salon.Entities.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Salon.ADO.DAL
{
    public class OrderRepository : ISalonManager<Order>
    {
        private readonly SqlConnection _connection;

        public OrderRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public Order Add(Order order)
        {
            Order newOrder = new Order
            {
                ServiceId = order.ServiceId,
                CustomerId = order.CustomerId,
                DateOfProcedure = order.DateOfProcedure,
                StatusId = order.StatusId
            };

            string sqlExpression = $"INSERT INTO Orders (ServiceId, CustomerId, DateOfProcedure, StatusId) " +
                                    $"VALUES (N'{newOrder.ServiceId}', " +
                                    $"N'{newOrder.CustomerId}', " +
                                    $"'{newOrder.DateOfProcedure}', " +
                                    $"N'{newOrder.StatusId}')";
            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();

            _connection.Close();

            return newOrder;
        }

        public void Delete(int id)
        {
            string sqlExpression = $"DELETE FROM Orders WHERE Id={id}";

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();

            _connection.Close();
        }

        public IEnumerable<Order> GetList()
        {
            string sqlExpression = "SELECT * FROM Orders";

            List<Order> orders = new List<Order>();

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Order order = new Order
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

            _connection.Close();

            return orders;
        }

        public Order GetSingle(int id)
        {
            string sqlExpression = $"SELECT * FROM Orders WHERE Id = {id}";

            Order order = new Order();

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    order = new Order
                    {
                        Id = reader.GetInt32(0),
                        ServiceId = reader.GetInt32(1),
                        CustomerId = reader.GetInt32(2),
                        DateOfProcedure = reader.GetDateTime(3),
                        StatusId = reader.GetInt32(4)
                    };
                }
            }

            _connection.Close();

            return order;
        }

        public Order Update(int id, Order order)
        {
            Order orderToUpdate = new Order
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

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();

            _connection.Close();

            return orderToUpdate;
        }

        public IEnumerable<OrderView> GetView()
        {
            string sqlExpression = "SELECT * FROM OrderTable ORDER BY [Date]";

            List<OrderView> orders = new List<OrderView>();

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    OrderView order = new OrderView
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

            _connection.Close();

            return orders;
        }

        public List<int> GetIds()
        {
            string sqlExpression = $"EXEC GetOrderIds;";

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
