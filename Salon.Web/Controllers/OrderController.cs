using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Salon.BLL.Interfaces;
using Salon.BLL.ViewModels;
using Salon.Web.Models;
using System.Collections.Generic;

namespace Salon.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderManager _orderManager;
        private readonly IServiceManager _serviceManager;
        private readonly ICustomerManager _customerManager;
        private readonly IStateManager _stateManager;

        public OrderController(IOrderManager orderManager, IServiceManager serviceManager, ICustomerManager customerManager, IStateManager stateManager)
        {
            _orderManager = orderManager;
            _serviceManager = serviceManager;
            _customerManager = customerManager;
            _stateManager = stateManager;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            var orders = _orderManager.Get(page);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderModel, OrderViewModel>());
            var mapper = new Mapper(config);
            var ordersVM = mapper.Map<List<OrderViewModel>>(orders.Order);

            var indexVM = new OrderIndexViewModel
            {
                Order = ordersVM,
                PageViewModel = orders.PageViewModel
            };

            return View(indexVM);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            var order = _orderManager.GetOrder((int)id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderModel, OrderViewModel>());
            var mapper = new Mapper(config);
            var orderVM = mapper.Map<OrderViewModel>(order);

            return View(orderVM);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var customers = _customerManager.Get();
            var services = _serviceManager.Get();
            var states = _stateManager.GetStates();
            var order = _orderManager.GetOrder((int)id);

            GlobalModel vm = new GlobalModel
            {
                Customer = customers.Customer,
                Service = services.Service,
                State = states,
                Id = order.Id,
                CustomerId = order.Customer.Id,
                ServiceId = order.Service.Id,
                Date = order.Date,
                StatusId = order.Status.Id
            };

            var config = new MapperConfiguration(cfg => cfg.CreateMap<GlobalModel, GlobalViewModel>());
            var mapper = new Mapper(config);
            var orderVM = mapper.Map<GlobalViewModel>(vm);

            return View(orderVM);
        }

        [HttpPost]
        public IActionResult Edit(GlobalModel order)
        {
            _orderManager.Update(order.Id, order);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            var customers = _customerManager.Get();
            var services = _serviceManager.Get();
            GlobalModel vm = new GlobalModel
            {
                Customer = customers.Customer,
                Service = services.Service
            };

            var config = new MapperConfiguration(cfg => cfg.CreateMap<GlobalModel, GlobalViewModel>());
            var mapper = new Mapper(config);
            var orderVM = mapper.Map<GlobalViewModel>(vm);

            return View(orderVM);
        }

        [HttpPost]
        public IActionResult Create(GlobalModel orderToCreate)
        {
            _orderManager.Add(orderToCreate);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Delete(int? id)
        {
            var order = _orderManager.GetOrder((int)id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderModel, OrderViewModel>());
            var mapper = new Mapper(config);
            var orderVM = mapper.Map<OrderViewModel>(order);

            return View(orderVM);
        }

        [HttpPost]
        public IActionResult Delete(OrderModel order)
        {
            var deletedOrder = _orderManager.Delete(order.Id);
            return RedirectToAction("Index");
        }
    }
}
