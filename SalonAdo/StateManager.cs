﻿using Microsoft.Data.SqlClient;
using SalonDAL.Models;
using SalonDAL.Models.Interfaces;
using System.Collections.Generic;

namespace SalonAdo
{
    public class StateManager : ISalonManager<State>
    {
        private readonly SqlConnection _connection;

        public StateManager(SqlConnection connection)
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

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();

            _connection.Close();

            return newState;
        }

        public void Delete(int id)
        {
            string sqlExpression = $"DELETE FROM States WHERE Id={id}";

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();

            _connection.Close();
        }

        public IEnumerable<State> GetList()
        {
            string sqlExpression = "SELECT * FROM States";

            List<State> states = new List<State>();

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
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

            _connection.Close();

            return states;
        }

        public State GetSingle(int id)
        {
            string sqlExpression = $"SELECT * FROM States WHERE Id = {id}";

            State state = new State();

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
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

            _connection.Close();

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

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();

            _connection.Close();

            return stateToUpdate;
        }
    }
}
