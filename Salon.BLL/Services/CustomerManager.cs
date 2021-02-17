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
        private ISalonRepository<CustomerEntity> _salonManager;
        private ISalonRepository<OrderEntity> _orderManager;

        public CustomerManager(ISalonRepository<CustomerEntity> salonManager, ISalonRepository<OrderEntity> orderManager)
        {
            _salonManager = salonManager;
            _orderManager = orderManager;
        }

        public CustomerModel Add(CustomerModel customer)
        {
            try
            {
                CustomerEntity newCustomer = new CustomerEntity
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email
                };

                CustomerEntity createdCustomer = _salonManager.Add(newCustomer);

                CustomerModel customerViewModel = new CustomerModel
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
            
        public string Delete(int id)
        {
            try
            {
                IEnumerable<int> listOfIds = _salonManager.GetIds();
                List<OrderEntity> listOfOrders = (List<OrderEntity>)_orderManager.GetList();
                if (listOfOrders.Any(c => c.CustomerId == id))
                {
                    return $"Customer can't be deleted because there are orders with this customer";
                }

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

        public CustomerModel GetCustomer(int id)
        {
            try
            {
                IEnumerable<int> listOfIds = _salonManager.GetIds();

                if (listOfIds.Contains(id))
                {
                    CustomerEntity selectedCustomer = _salonManager.GetSingle(id);

                    CustomerModel customerViewModel = new CustomerModel
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

        public CustomerIndexModel Get(int page = 1)
        {
            try
            {
                IEnumerable<CustomerEntity> customers = _salonManager.GetList();

                var count = customers.Count();

                int pageSize = 8;

                var items = customers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                List<CustomerModel> customersVM = new List<CustomerModel>();
                foreach (CustomerEntity c in items)
                {
                    customersVM.Add(new CustomerModel
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        PhoneNumber = c.PhoneNumber,
                        Email = c.Email
                    });
                }

                PageModel pageViewModel = new PageModel(count, page, pageSize);
                CustomerIndexModel viewModel = new CustomerIndexModel
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

        public CustomerIndexModel Get()
        {
            try
            {
                IEnumerable<CustomerEntity> customers = _salonManager.GetList();

                List<CustomerModel> customersVM = new List<CustomerModel>();
                foreach (CustomerEntity c in customers)
                {
                    customersVM.Add(new CustomerModel
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        PhoneNumber = c.PhoneNumber,
                        Email = c.Email
                    });
                }

                CustomerIndexModel viewModel = new CustomerIndexModel
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

        public CustomerModel Update(int id, CustomerModel customer)
        {
            try
            {
                CustomerEntity customerSelected = _salonManager.GetSingle(id);


                CustomerEntity customerToUpdate = new CustomerEntity
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email
                };

                CustomerEntity updatedCustomer = _salonManager.Update(id, customerToUpdate);


                CustomerModel customerViewModel = new CustomerModel
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
