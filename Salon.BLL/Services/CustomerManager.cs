using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL;
using Salon.BLL.Interfaces;
using Salon.Entities.Models;
using Salon.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.BLL.Services
{
    public class CustomerManager : ICustomerManager
    {
        public Salon.DTO.Customer AddCustomer(Salon.DTO.Customer customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Salon.Entities.Models.Customer> salon = new CustomerRepository(connection);

                    //CustomerRepository checkUniqueness = new CustomerRepository(connection);
                    //IEnumerable<string> listOfPhones = checkUniqueness.GetPhoneNumbers();
                    //IEnumerable<string> listOfEmails = checkUniqueness.GetEmails();

                    //if (listOfPhones.Contains(customer.PhoneNumber))
                    //{
                    //    throw new Exception("This phone number is already taken");
                    //}

                    //if (listOfEmails.Contains(customer.Email))
                    //{
                    //    throw new Exception("This email is already taken");
                    //}

                    Salon.Entities.Models.Customer newCustomer = new Salon.Entities.Models.Customer
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        PhoneNumber = customer.PhoneNumber,
                        Email = customer.Email
                    };

                    Salon.Entities.Models.Customer createdCustomer = salon.Add(newCustomer);


                    Salon.DTO.Customer customerDTO = new Salon.DTO.Customer
                    {
                        Id = createdCustomer.Id,
                        FirstName = createdCustomer.FirstName,
                        LastName = createdCustomer.LastName,
                        PhoneNumber = createdCustomer.PhoneNumber,
                        Email = createdCustomer.Email
                    };

                    return customerDTO;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
            
        public string DeleteCustomer(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Salon.Entities.Models.Customer> salon = new CustomerRepository(connection);

                    CustomerRepository customerRepository = new CustomerRepository(connection);
                    IEnumerable<int> listOfIds = customerRepository.GetIds();

                    if (listOfIds.Contains(id))
                    {
                        salon.Delete(id);
                        return $"Customer with id {id} deleted";
                    }
                    else
                    {
                        throw new Exception($"Customer with id {id} doesen't found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Salon.DTO.Customer GetCustomer(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Salon.Entities.Models.Customer> salon = new CustomerRepository(connection);

                    CustomerRepository customerRepository = new CustomerRepository(connection);
                    IEnumerable<int> listOfIds = customerRepository.GetIds();

                    if (listOfIds.Contains(id))
                    {
                        Salon.Entities.Models.Customer selectedCustomer = salon.GetSingle(id);

                        Salon.DTO.Customer customerDTO = new DTO.Customer
                        {
                            Id = selectedCustomer.Id,
                            FirstName = selectedCustomer.FirstName,
                            LastName = selectedCustomer.LastName,
                            PhoneNumber = selectedCustomer.PhoneNumber,
                            Email = selectedCustomer.Email
                        };

                        return customerDTO;
                    }
                    else
                    {
                        throw new Exception($"Customer with id {id} doesen't found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Salon.DTO.Customer> GetCustomers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Salon.Entities.Models.Customer> salon = new CustomerRepository(connection);

                    IEnumerable<Entities.Models.Customer> customers = salon.GetList();

                    List<DTO.Customer> customersDTO = new List<DTO.Customer>();

                    foreach (Entities.Models.Customer c in customers)
                    {
                        customersDTO.Add(new DTO.Customer
                        {
                            Id = c.Id,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            PhoneNumber = c.PhoneNumber,
                            Email = c.Email
                        });
                    }

                    return customersDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Salon.DTO.Customer UpdateCustomer(int id, Salon.DTO.Customer customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Salon.Entities.Models.Customer> salon = new CustomerRepository(connection);
                    Entities.Models.Customer customerSelected = salon.GetSingle(id);

                    //CustomerRepository customerRepository = new CustomerRepository(connection);
                    //IEnumerable<int> listOfIds = customerRepository.GetIds();
                    //IEnumerable<string> listOfPhones = customerRepository.GetPhoneNumbers();
                    //IEnumerable<string> listOfEmails = customerRepository.GetEmails();

                    //if(customerSelected.PhoneNumber != customer.PhoneNumber)
                    //{
                    //    if (listOfPhones.Contains(customer.PhoneNumber))
                    //    {
                    //        throw new Exception("This phone number is already taken");
                    //    }
                    //}


                    //if (customerSelected.Email != customer.Email)
                    //{
                    //    if (listOfEmails.Contains(customer.Email))
                    //    {
                    //        throw new Exception("This email is already taken");
                    //    }
                    //}


                    Salon.Entities.Models.Customer customerToUpdate = new Entities.Models.Customer
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        PhoneNumber = customer.PhoneNumber,
                        Email = customer.Email
                    };

                    Entities.Models.Customer updatedCustomer = salon.Update(id, customerToUpdate);

                    DTO.Customer updatedCustomerDTO = new DTO.Customer 
                    {
                        Id = updatedCustomer.Id,
                        FirstName = updatedCustomer.FirstName,
                        LastName = updatedCustomer.LastName,
                        PhoneNumber = updatedCustomer.PhoneNumber,
                        Email = updatedCustomer.Email
                    };
                    return updatedCustomerDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
