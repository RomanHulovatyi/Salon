using Salon.Abstractions.Interfaces;
using Salon.BLL.Interfaces;
using Salon.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Salon.BLL.ViewModels;

namespace Salon.BLL.Services
{
    public class CustomerManager : ICustomerManager
    {
        private ISalonManager<Customer> _salonManager;

        public CustomerManager(ISalonManager<Customer> salonManager)
        {
            _salonManager = salonManager;
        }

        public CustomerViewModel AddCustomer(CustomerViewModel customer)
        {
            try
            {
                Customer newCustomer = new Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email
                };

                Customer createdCustomer = _salonManager.Add(newCustomer);

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
            catch
            {
                throw;
            }
        }
            
        public string DeleteCustomer(int id)
        {
            try
            {
                IEnumerable<int> listOfIds = _salonManager.GetIds();

                if (listOfIds.Contains(id))
                {
                    _salonManager.Delete(id);
                    return $"Customer with id {id} deleted";
                }
                else
                {
                    throw new Exception($"Customer with id {id} doesen't found");
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
                IEnumerable<int> listOfIds = _salonManager.GetIds();

                if (listOfIds.Contains(id))
                {
                    Customer selectedCustomer = _salonManager.GetSingle(id);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IndexViewModel GetCustomers(int page = 1)
        {
            try
            {
                IEnumerable<Customer> customers = _salonManager.GetList();

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IndexViewModel GetCustomers()
        {
            try
            {
                IEnumerable<Customer> customers = _salonManager.GetList();

                List<CustomerViewModel> customersVM = new List<CustomerViewModel>();
                foreach (Customer c in customers)
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

                IndexViewModel viewModel = new IndexViewModel
                {
                    Customer = customersVM.OrderBy(x => x.FirstName)
                };

                return viewModel;
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
                Customer customerSelected = _salonManager.GetSingle(id);


                Customer customerToUpdate = new Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email
                };

                Customer updatedCustomer = _salonManager.Update(id, customerToUpdate);


                CustomerViewModel customerViewModel = new CustomerViewModel
                {
                    FirstName = updatedCustomer.FirstName,
                    LastName = updatedCustomer.LastName,
                    PhoneNumber = updatedCustomer.PhoneNumber,
                    Email = updatedCustomer.Email
                };

                return customerViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
