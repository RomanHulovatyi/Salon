using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL.Connection;
using Salon.Entities.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Salon.ADO.DAL
{
    public class StateRepository : ISalonManager<State>
    {
        private readonly ISqlConnectionFactory _connection;

        public StateRepository(ISqlConnectionFactory connection)
        {
            _connection = connection;
        }

        public State Add(State state)
        {
            State newState = new State
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

        public IEnumerable<State> GetList()
        {
            string sqlExpression = "SELECT * FROM States";

            List<State> states = new List<State>();

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    State state = new State
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

        public State GetSingle(int id)
        {
            string sqlExpression = $"SELECT * FROM States WHERE Id = {id}";

            State state = new State();

            SqlConnection sql = _connection.CreateSqlConnection();
            sql.Open();

            SqlCommand command = new SqlCommand(sqlExpression, sql);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    state = new State
                    {
                        Id = reader.GetInt32(0),
                        OrderStatus = reader.GetString(1)
                    };
                }
            }

            sql.Close();

            return state;
        }

        public State Update(int id, State state)
        {
            State stateToUpdate = new State
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
