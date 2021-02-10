using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL;
using Salon.BLL.Interfaces;
using Salon.BLL.ViewModels;
using Salon.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.BLL.Services
{
    public class OrderManager : IOrderManager
    {
        //public IEnumerable<CustomerViewModel> GetCustomers()
        //{

        //}
        public void AddOrder(GlobalViewModel order)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Order> salon = new OrderRepository(connection);

                    Order newOrder = new Order
                    {
                        ServiceId = order.ServiceId,
                        CustomerId = order.CustomerId,
                        DateOfProcedure = order.Date,
                        StatusId = 6
                    };

                    Order createdOrder = salon.Add(newOrder);

                    //OrderViewModel orderViewModel = new OrderViewModel
                    //{
                    //    Id = createdOrder.Id,
                    //    Service = createdOrder.ServiceId,
                    //    LastName = createdOrder.LastName,
                    //    PhoneNumber = createdOrder.PhoneNumber,
                    //    Email = createdOrder.Email
                    //};

                    //return customerViewModel;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string DeleteOrder(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Order> salon = new OrderRepository(connection);

                    OrderRepository orderRepository = new OrderRepository(connection);
                    IEnumerable<int> listOfIds = orderRepository.GetIds();

                    if (listOfIds.Contains(id))
                    {
                        salon.Delete(id);
                        return $"Order with id {id} deleted";
                    }
                    else
                    {
                        throw new Exception($"Order with id {id} doesen't found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OrderViewModel GetOrder(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    OrderRepository orderRepository = new OrderRepository(connection);
                    IEnumerable<int> listOfIds = orderRepository.GetIds();

                    CustomerRepository customerRepository = new CustomerRepository(connection);
                    ServiceRepository serviceRepository = new ServiceRepository(connection);
                    StateRepository stateRepository = new StateRepository(connection);


                    if (listOfIds.Contains(id))
                    {
                        Order selectedOrder = orderRepository.GetSingle(id);

                        Customer customer = customerRepository.GetSingle(selectedOrder.CustomerId);
                        Service service = serviceRepository.GetSingle(selectedOrder.ServiceId);
                        State state = stateRepository.GetSingle(selectedOrder.StatusId);

                        OrderViewModel orderViewModel = new OrderViewModel
                        {
                            Id = selectedOrder.Id,
                            Customer = new CustomerViewModel 
                            {
                                Id = customer.Id,
                                FirstName = customer.FirstName,
                                LastName = customer.LastName,
                                PhoneNumber = customer.PhoneNumber,
                                Email = customer.Email
                            },
                            Service = new ServiceViewModel 
                            {
                                Id = service.Id,
                                NameOfService = service.NameOfService,
                                Price = service.Price
                            },
                            Price = service.Price,
                            Date = selectedOrder.DateOfProcedure,
                            Status = new StateViewModel
                            {
                                Id = state.Id,
                                OrderStatus = state.OrderStatus
                            }
                        };

                        return orderViewModel;
                    }
                    else
                    {
                        throw new Exception($"Order with id {id} doesen't found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OrderIndexViewModel GetOrders(int page)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    OrderRepository salon = new OrderRepository(connection);


                    IEnumerable<Order> orders = salon.GetList().OrderByDescending(x => x.DateOfProcedure);

                    var count = orders.Count();

                    int pageSize = 8;

                    var items = orders.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    List<OrderViewModel> ordersVM = new List<OrderViewModel>();
                    foreach (Order c in items)
                    {

                        CustomerRepository customerRepository = new CustomerRepository(connection);
                        ServiceRepository serviceRepository = new ServiceRepository(connection);
                        StateRepository stateRepository = new StateRepository(connection);
                        Customer customer = customerRepository.GetSingle(c.CustomerId);
                        Service service = serviceRepository.GetSingle(c.ServiceId);
                        State state = stateRepository.GetSingle(c.StatusId);

                        ordersVM.Add(new OrderViewModel
                        {
                            Id = c.Id,
                            Customer = new CustomerViewModel
                            {
                                Id = customer.Id,
                                FirstName = customer.FirstName,
                                LastName = customer.LastName,
                                PhoneNumber = customer.PhoneNumber,
                                Email = customer.Email
                            },
                            Service = new ServiceViewModel
                            {
                                Id = service.Id,
                                NameOfService = service.NameOfService,
                                Price = service.Price
                            },
                            Price = service.Price,
                            Date = c.DateOfProcedure,
                            Status = new StateViewModel
                            {
                                Id = state.Id,
                                OrderStatus = state.OrderStatus
                            }
                        });
                    }

                    PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                    OrderIndexViewModel viewModel = new OrderIndexViewModel
                    {
                        PageViewModel = pageViewModel,
                        Order = ordersVM.OrderByDescending(x => x.Date)
                    };

                    return viewModel;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrder(int id, GlobalViewModel order)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Order> salon = new OrderRepository(connection);
                    Order customerSelected = salon.GetSingle(id);

                    Order orderToUpdate = new Order
                    {
                        ServiceId = order.ServiceId,
                        CustomerId = order.CustomerId,
                        DateOfProcedure = order.Date,
                        StatusId = order.StatusId,
                    };

                    Order updatedOrder = salon.Update(id, orderToUpdate);


                    //OrderViewModel orderViewModel = new OrderViewModel
                    //{
                    //    FirstName = updatedCustomer.FirstName,
                    //    LastName = updatedCustomer.LastName,
                    //    PhoneNumber = updatedCustomer.PhoneNumber,
                    //    Email = updatedCustomer.Email
                    //};

                    //return customerViewModel;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
