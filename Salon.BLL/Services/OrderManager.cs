using Salon.Abstractions.Interfaces;
using Salon.BLL.Interfaces;
using Salon.BLL.ViewModels;
using Salon.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Salon.BLL.Services
{
    public class OrderManager : IOrderManager
    {
        private ISalonManager<Order> _orderManager;
        private ISalonManager<Customer> _customerManager;
        private ISalonManager<Service> _serviceManager;
        private ISalonManager<State> _stateManager;
        public OrderManager(ISalonManager<Order> orderManager, 
                            ISalonManager<Customer> customerManager, 
                            ISalonManager<Service> servcieManager,
                            ISalonManager<State> stateManager)
        {
            _orderManager = orderManager;
            _customerManager = customerManager;
            _serviceManager = servcieManager;
            _stateManager = stateManager;
        }
        public void AddOrder(GlobalViewModel order)
        {
            try
            {
                Order newOrder = new Order
                {
                    ServiceId = order.ServiceId,
                    CustomerId = order.CustomerId,
                    DateOfProcedure = order.Date,
                    StatusId = 6
                };

                Order createdOrder = _orderManager.Add(newOrder);
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
                IEnumerable<int> listOfIds = _orderManager.GetIds();

                if (listOfIds.Contains(id))
                {
                    _orderManager.Delete(id);
                    return $"Order with id {id} deleted";
                }
                else
                {
                    throw new Exception($"Order with id {id} doesen't found");
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
                IEnumerable<int> listOfIds = _orderManager.GetIds();

                if (listOfIds.Contains(id))
                {
                    Order selectedOrder = _orderManager.GetSingle(id);

                    Customer customer = _customerManager.GetSingle(selectedOrder.CustomerId);
                    Service service = _serviceManager.GetSingle(selectedOrder.ServiceId);
                    State state = _stateManager.GetSingle(selectedOrder.StatusId);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OrderIndexViewModel GetOrders(int page)
        {
            try
            {
                IEnumerable<Order> orders = _orderManager.GetList().OrderByDescending(x => x.DateOfProcedure);

                var count = orders.Count();

                int pageSize = 8;

                var items = orders.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                List<OrderViewModel> ordersVM = new List<OrderViewModel>();
                foreach (Order c in items)
                {
                    Customer customer = _customerManager.GetSingle(c.CustomerId);
                    Service service = _serviceManager.GetSingle(c.ServiceId);
                    State state = _stateManager.GetSingle(c.StatusId);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrder(int id, GlobalViewModel order)
        {
            try
            {
                Order customerSelected = _orderManager.GetSingle(id);

                Order orderToUpdate = new Order
                {
                    ServiceId = order.ServiceId,
                    CustomerId = order.CustomerId,
                    DateOfProcedure = order.Date,
                    StatusId = order.StatusId,
                };

                Order updatedOrder = _orderManager.Update(id, orderToUpdate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
