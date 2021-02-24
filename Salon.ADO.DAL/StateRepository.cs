using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL.Connection;
using Salon.Entities.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Salon.ADO.DAL
{
    public class StateRepository : ISalonRepository<StateEntity>
    {
        private readonly ISqlConnectionFactory _connection;

        public StateRepository(ISqlConnectionFactory connection)
        {
            _connection = connection;
        }

        public StateEntity Add(StateEntity state)
        {
            StateEntity newState = new StateEntity
            {
                OrderStatus = state.OrderStatus
            };

            string sqlExpression = $"INSERT INTO States (OrderStatus) VALUES (N'{newState.OrderStatus})'";

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            command.ExecuteNonQuery();

            sql.Close();

            return newState;
        }

        public void Delete(int id)
        {
            string sqlExpression = $"DELETE FROM States WHERE Id={id}";

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            command.ExecuteNonQuery();

            sql.Close();
        }

        public IEnumerable<StateEntity> GetList()
        {
            string sqlExpression = "SELECT * FROM States";

            List<StateEntity> states = new List<StateEntity>();

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    StateEntity state = new StateEntity
                    {
                        Id = reader.GetInt32(0),
                        OrderStatus = reader.GetString(1)
                    };

                    states.Add(state);
                }
            }

            sql.Close();

            return states;
        }

        public StateEntity GetSingle(int id)
        {
            string sqlExpression = $"SELECT * FROM States WHERE Id = {id}";

            StateEntity state = new StateEntity();

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    state = new StateEntity
                    {
                        Id = reader.GetInt32(0),
                        OrderStatus = reader.GetString(1)
                    };
                }
            }

            sql.Close();

            return state;
        }

        public StateEntity Update(int id, StateEntity state)
        {
            StateEntity stateToUpdate = new StateEntity
            {
                Id = id,
                OrderStatus = state.OrderStatus
            };

            string sqlExpression = "UPDATE States " +
                                            $"SET OrderStatus = N'{stateToUpdate.OrderStatus}' WHERE Id={id}";

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            command.ExecuteNonQuery();

            sql.Close();

            return stateToUpdate;
        }

        public List<int> GetIds()
        {
            string sqlExpression = $"EXEC GetStateIds;";

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
