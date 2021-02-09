using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL;
using Salon.BLL.Interfaces;
using Salon.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Salon.BLL.ViewModels;

namespace Salon.BLL.Services
{
    public class CustomerManager : ICustomerManager
    {
        public CustomerViewModel AddCustomer(CustomerViewModel customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Customer> salon = new CustomerRepository(connection);

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

                    Customer newCustomer = new Customer
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        PhoneNumber = customer.PhoneNumber,
                        Email = customer.Email
                    };

                    Customer createdCustomer = salon.Add(newCustomer);

                    CustomerViewModel customerViewModel = new CustomerViewModel
                    {
                        Id = createdCustomer.Id,
                        FirstName = createdCustomer.FirstName,
                        LastName = createdCustomer.LastName,
                        PhoneNumber = createdCustomer.PhoneNumber,
                        Email = createdCustomer.Email
                    };

                    return customerViewModel;
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
                    ISalonManager<Customer> salon = new CustomerRepository(connection);

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

        public CustomerViewModel GetCustomer(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Customer> salon = new CustomerRepository(connection);

                    CustomerRepository customerRepository = new CustomerRepository(connection);
                    IEnumerable<int> listOfIds = customerRepository.GetIds();

                    if (listOfIds.Contains(id))
                    {
                        Customer selectedCustomer = salon.GetSingle(id);

                        CustomerViewModel customerViewModel = new CustomerViewModel
                        {
                            Id = selectedCustomer.Id,
                            FirstName = selectedCustomer.FirstName,
                            LastName = selectedCustomer.LastName,
                            PhoneNumber = selectedCustomer.PhoneNumber,
                            Email = selectedCustomer.Email
                        };

                        return customerViewModel;
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

        public IndexViewModel GetCustomers(int page = 1)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Customer> salon = new CustomerRepository(connection);

                    IEnumerable<Customer> customers = salon.GetList();

                    var count = customers.Count();

                    int pageSize = 8;

                    var items = customers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    List<CustomerViewModel> customersVM = new List<CustomerViewModel>();
                    foreach (Customer c in items)
                    {
                        customersVM.Add(new CustomerViewModel
                        {
                            Id = c.Id,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            PhoneNumber = c.PhoneNumber,
                            Email = c.Email
                        });
                    }

                    PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                    IndexViewModel viewModel = new IndexViewModel
                    {
                        PageViewModel = pageViewModel,
                        Customer = customersVM
                    };

                    return viewModel;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CustomerViewModel UpdateCustomer(int id, CustomerViewModel customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Customer> salon = new CustomerRepository(connection);
                    Customer customerSelected = salon.GetSingle(id);

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


                    Customer customerToUpdate = new Customer
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        PhoneNumber = customer.PhoneNumber,
                        Email = customer.Email
                    };

                    Customer updatedCustomer = salon.Update(id, customerToUpdate);


                    CustomerViewModel customerViewModel = new CustomerViewModel
                    {
                        FirstName = updatedCustomer.FirstName,
                        LastName = updatedCustomer.LastName,
                        PhoneNumber = updatedCustomer.PhoneNumber,
                        Email = updatedCustomer.Email
                    };

                    return customerViewModel;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
