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
        private ISalonRepository<OrderEntity> _orderManager;
        private ISalonRepository<CustomerEntity> _customerManager;
        private ISalonRepository<ServiceEntity> _serviceManager;
        private ISalonRepository<StateEntity> _stateManager;
        public OrderManager(ISalonRepository<OrderEntity> orderManager, 
                            ISalonRepository<CustomerEntity> customerManager, 
                            ISalonRepository<ServiceEntity> servcieManager,
                            ISalonRepository<StateEntity> stateManager)
        {
            _orderManager = orderManager;
            _customerManager = customerManager;
            _serviceManager = servcieManager;
            _stateManager = stateManager;
        }
        public void Add(GlobalModel order)
        {
            try
            {
                OrderEntity newOrder = new OrderEntity
                {
                    ServiceId = order.ServiceId,
                    CustomerId = order.CustomerId,
                    DateOfProcedure = order.Date,
                    StatusId = 6
                };

                OrderEntity createdOrder = _orderManager.Add(newOrder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Delete(int id)
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

        public OrderModel GetOrder(int id)
        {
            try
            {
                IEnumerable<int> listOfIds = _orderManager.GetIds();

                if (listOfIds.Contains(id))
                {
                    OrderEntity selectedOrder = _orderManager.GetSingle(id);

                    CustomerEntity customer = _customerManager.GetSingle(selectedOrder.CustomerId);
                    ServiceEntity service = _serviceManager.GetSingle(selectedOrder.ServiceId);
                    StateEntity state = _stateManager.GetSingle(selectedOrder.StatusId);

                    OrderModel orderViewModel = new OrderModel
                    {
                        Id = selectedOrder.Id,
                        Customer = new CustomerModel 
                        {
                            Id = customer.Id,
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                            PhoneNumber = customer.PhoneNumber,
                            Email = customer.Email
                        },
                        Service = new ServiceModel 
                        {
                            Id = service.Id,
                            NameOfService = service.NameOfService,
                            Price = service.Price
                        },
                        Price = service.Price,
                        Date = selectedOrder.DateOfProcedure,
                        Status = new StateModel
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

        public OrderIndexModel Get(int page)
        {
            try
            {
                IEnumerable<OrderEntity> orders = _orderManager.GetList().OrderByDescending(x => x.DateOfProcedure);

                var count = orders.Count();

                int pageSize = 8;

                var items = orders.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                List<OrderModel> ordersVM = new List<OrderModel>();
                foreach (OrderEntity c in items)
                {
                    CustomerEntity customer = _customerManager.GetSingle(c.CustomerId);
                    ServiceEntity service = _serviceManager.GetSingle(c.ServiceId);
                    StateEntity state = _stateManager.GetSingle(c.StatusId);

                    ordersVM.Add(new OrderModel
                    {
                        Id = c.Id,
                        Customer = new CustomerModel
                        {
                            Id = customer.Id,
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                            PhoneNumber = customer.PhoneNumber,
                            Email = customer.Email
                        },
                        Service = new ServiceModel
                        {
                            Id = service.Id,
                            NameOfService = service.NameOfService,
                            Price = service.Price
                        },
                        Price = service.Price,
                        Date = c.DateOfProcedure,
                        Status = new StateModel
                        {
                            Id = state.Id,
                            OrderStatus = state.OrderStatus
                        }
                    });
                }

                PageModel pageViewModel = new PageModel(count, page, pageSize);
                OrderIndexModel viewModel = new OrderIndexModel
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

        public void Update(int id, GlobalModel order)
        {
            try
            {
                OrderEntity customerSelected = _orderManager.GetSingle(id);

                OrderEntity orderToUpdate = new OrderEntity
                {
                    ServiceId = order.ServiceId,
                    CustomerId = order.CustomerId,
                    DateOfProcedure = order.Date,
                    StatusId = order.StatusId,
                };

                OrderEntity updatedOrder = _orderManager.Update(id, orderToUpdate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
