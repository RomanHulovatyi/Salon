
using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL;
using Salon.Entities.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace Salon.Services.AdoAproach
{
    public class ManageCustomers
    {
        public static void GetList()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    Console.WriteLine("List of customers:");
                    Console.WriteLine("{0, 5} {1, 20} {2, 20} {3, 15} {4, 30}", "ID", "Name", "Surname", "Phone number", "Email");

                    ISalonManager<Customer> customerManager = new CustomerRepository(connection);
                    IEnumerable<Customer> listOfCustomers = customerManager.GetList();

                    foreach (Salon.Entities.Models.Customer c in listOfCustomers)
                    {
                        Console.WriteLine("{0,5} {1,20} {2,20} {3,15} {4,30}", c.Id, c.FirstName, c.LastName, c.PhoneNumber, c.Email);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong... Try latter");
                Console.WriteLine(ex.Message);
            }
        }

        public static void Add()
        {
            try
            {

                Customer customer = new Customer();

                Console.WriteLine("Please enter the following information:");

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    ISalonManager<Customer> customerManager = new CustomerRepository(connection);

                    CustomerRepository checkUniqueness = new CustomerRepository(connection);
                    IEnumerable<string> listOfPhones = checkUniqueness.GetPhoneNumbers();
                    IEnumerable<string> listOfEmails = checkUniqueness.GetEmails();


                    Console.Write("Name: ");
                    customer.FirstName = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(customer.FirstName))
                    {
                        Console.Write($"Please enter correct {nameof(customer.FirstName)}");
                        customer.FirstName = Console.ReadLine();
                    }

                    Console.Write("Last name: ");
                    customer.LastName = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(customer.LastName))
                    {
                        Console.Write("Please enter correct last name:");
                        customer.LastName = Console.ReadLine();
                    }

                    Console.Write("Phone number: ");
                    string phone = Console.ReadLine();
                    Regex numberPattern = new Regex(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$");
                    while (!numberPattern.IsMatch(phone))
                    {
                        Console.Write("Wrong number! Try again: ");
                        phone = Console.ReadLine();
                    }
                    while (listOfPhones.Contains(phone))
                    {
                        Console.Write("This number is already taken! Try another one: ");
                        phone = Console.ReadLine();
                    }
                    customer.PhoneNumber = phone;

                    Console.Write("Email:");
                    string email = Console.ReadLine();
                    while (!EmailValidation.IsValidEmail(email))
                    {
                        Console.WriteLine("Wrong email!");
                        Console.Write("Try again: ");
                        email = Console.ReadLine();
                    }
                    while (listOfEmails.Contains(email))
                    {
                        Console.Write("This email is already taken! Try another one: ");
                        email = Console.ReadLine();
                    }
                    customer.Email = email;

                    Customer addedCustomer = customerManager.Add(customer);
                }


                Console.WriteLine($"Customer {customer.FirstName} {customer.LastName} successfully added!");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong... Try latter");
                Console.WriteLine(ex.Message);
            }
        }

        public static void Update()
        {
            try
            {
                Console.WriteLine("Please select customer to update:");
                GetList();

                Console.Write("Enter ID of customer you want to update:");
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    ISalonManager<Customer> customerManager = new CustomerRepository(connection);

                    CustomerRepository checkUniqueness = new CustomerRepository(connection);
                    IEnumerable<int> listOfIDs = checkUniqueness.GetIds();
                    IEnumerable<string> listOfPhones = checkUniqueness.GetPhoneNumbers();
                    IEnumerable<string> listOfEmails = checkUniqueness.GetEmails();

                    
                    string idToUpdate = Console.ReadLine();
                    int idOfCustomer;

                    while (!Int32.TryParse(idToUpdate, out idOfCustomer) || !listOfIDs.Contains(idOfCustomer))
                    {
                        Console.WriteLine($"Customer with ID {idOfCustomer} dosent found. Try again: ");
                        idToUpdate = Console.ReadLine();
                    }

                    Customer selectedCustomer = customerManager.GetSingle(idOfCustomer);

                    Customer customerToUpdate = new Customer();

                    Console.WriteLine("Select what you want to change:");
                    Console.WriteLine($"1. Change first name");
                    Console.WriteLine($"2. Change last name");
                    Console.WriteLine($"3. Change phone number");
                    Console.WriteLine($"4. Change email");
                    Console.Write("Enter number of action:");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Console.Write($"Change first name from {selectedCustomer.FirstName} to: ");
                            customerToUpdate.FirstName = Console.ReadLine();
                            while (string.IsNullOrWhiteSpace(customerToUpdate.FirstName))
                            {
                                Console.Write("Please enter correct name:");
                                customerToUpdate.FirstName = Console.ReadLine();
                            }

                            customerToUpdate.LastName = selectedCustomer.LastName;
                            customerToUpdate.PhoneNumber = selectedCustomer.PhoneNumber;
                            customerToUpdate.Email = selectedCustomer.Email;
                            break;

                        case "2":
                            Console.WriteLine($"Change last name from {selectedCustomer.LastName} to: ");
                            customerToUpdate.FirstName = selectedCustomer.FirstName;
                            customerToUpdate.LastName = Console.ReadLine();
                            while (string.IsNullOrWhiteSpace(customerToUpdate.LastName))
                            {
                                Console.Write("Please enter correct name:");
                                customerToUpdate.LastName = Console.ReadLine();
                            }

                            customerToUpdate.PhoneNumber = selectedCustomer.PhoneNumber;
                            customerToUpdate.Email = selectedCustomer.Email;
                            break;

                        case "3":
                            Console.WriteLine($"Change phone number from {selectedCustomer.PhoneNumber} to: ");
                            customerToUpdate.FirstName = selectedCustomer.FirstName;
                            customerToUpdate.LastName = selectedCustomer.LastName;

                            string phone = Console.ReadLine();
                            Regex numberPattern = new Regex(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$");
                            while (!numberPattern.IsMatch(phone))
                            {
                                Console.Write("Wrong number! Try again: ");
                                phone = Console.ReadLine();
                            }
                            while (listOfPhones.Contains(phone))
                            {
                                Console.Write("This number is already taken! Try another one: ");
                                phone = Console.ReadLine();
                            }
                            customerToUpdate.PhoneNumber = phone;

                            customerToUpdate.Email = selectedCustomer.Email;
                            break;

                        case "4":
                            Console.WriteLine($"Change email from {selectedCustomer.Email} to: ");
                            customerToUpdate.FirstName = selectedCustomer.FirstName;
                            customerToUpdate.LastName = selectedCustomer.LastName;
                            customerToUpdate.PhoneNumber = selectedCustomer.PhoneNumber;

                            string email = Console.ReadLine();
                            while (!EmailValidation.IsValidEmail(email))
                            {
                                Console.WriteLine("Wrong email!");
                                Console.Write("Try again: ");
                                email = Console.ReadLine();
                            }
                            while (listOfEmails.Contains(email))
                            {
                                Console.Write("This email is already taken! Try another one: ");
                                email = Console.ReadLine();
                            }
                            customerToUpdate.Email = email;
                            break;

                        default:
                            Console.WriteLine("Wrong number, try again!");
                            Update();
                            break;
                    }

                    Customer customer = customerManager.Update(idOfCustomer, customerToUpdate);

                    Console.WriteLine($"Customer {customer.FirstName} {customer.LastName} updated!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong... Try latter");
                Console.WriteLine(ex.Message);
            }
        }

        public static void Delete()
        {
            try
            {
                Console.WriteLine("Please select customer to delete:");
                GetList();

                Console.Write("Enter ID of customer you want to delete:");
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    ISalonManager<Customer> customerManager = new CustomerRepository(connection);

                    CustomerRepository checkIds = new CustomerRepository(connection);
                    IEnumerable<int> listOfIDs = checkIds.GetIds();

                    string idToDelete = Console.ReadLine();
                    int idOfCustomer;

                    while (!Int32.TryParse(idToDelete, out idOfCustomer) || !listOfIDs.Contains(idOfCustomer))
                    {
                        Console.WriteLine($"Customer with ID {idOfCustomer} dosent found. Try again: ");
                        idToDelete = Console.ReadLine();
                    }

                    customerManager.Delete(idOfCustomer);

                    Console.WriteLine($"Customer with ID {idToDelete} deleted.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong... Try latter");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
