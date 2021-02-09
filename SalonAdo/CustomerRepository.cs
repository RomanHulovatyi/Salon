﻿using Salon.Abstractions.Interfaces;
using Salon.Entities.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Salon.ADO.DAL
{
    public class CustomerRepository : ISalonManager<Customer>
    {
        private readonly SqlConnection _connection;

        public CustomerRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public Customer Add(Customer customer)
        {
            Customer newCustomer = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email
            };

            string sqlExpression = $"INSERT INTO Customers (FirstName, LastName, PhoneNumber, Email) " +
                                    $"VALUES (N'{customer.FirstName}', " +
                                    $"N'{customer.LastName}', " +
                                    $"'{customer.PhoneNumber}', " +
                                    $"N'{customer.Email}')";
            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();

            _connection.Close();

            return newCustomer;
        }

        public void Delete(int id)
        {
            string sqlExpression = $"DELETE FROM Customers WHERE Id={id}";

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();

            _connection.Close();
        }

        public IEnumerable<Customer> GetList()
        {
            string sqlExpression = "SELECT * FROM Customers";

            List<Customer> customers = new List<Customer>();

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Customer customer = new Customer
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4)
                    };

                    customers.Add(customer);
                }
            }

            _connection.Close();

            return customers;
        }

        public Customer GetSingle(int id)
        {
            string sqlExpression = $"SELECT * FROM Customers WHERE Id = {id}";

            Customer customer = new Customer();

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    customer = new Customer
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4)
                    };
                }
            }

            _connection.Close();

            return customer;
        }

        public Customer Update(int id, Customer customer)
        {
            Customer customerToUpdate = new Customer
            {
                Id = id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email
            };

            string sqlExpression = "UPDATE Customers " +
                                            $"SET FirstName = N'{customer.FirstName}'," +
                                            $"LastName = N'{customer.LastName}'," +
                                            $"PhoneNumber = '{customer.PhoneNumber}'," +
                                            $"Email = N'{customer.Email}' " +
                                            $"WHERE Id={id}";

            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();

            _connection.Close();

            return customerToUpdate;
        }

        public IEnumerable<string> GetPhoneNumbers()
        {
            string sqlExpression = $"EXEC GetPhoneNumbers;";

            List<string> phoneNumbers = new List<string>();
            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string phoneNumber = reader.GetString(0);
                    phoneNumbers.Add(phoneNumber);
                }
            }
            _connection.Close();

            return phoneNumbers;
        }

        public IEnumerable<string> GetEmails()
        {
            string sqlExpression = $"EXEC GetEmails;";

            List<string> emails = new List<string>();
            _connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string email = reader.GetString(0);
                    emails.Add(email);
                }
            }
            _connection.Close();

            return emails;
        }

        public List<int> GetIds()
        {
            string sqlExpression = $"EXEC GetIds;";

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