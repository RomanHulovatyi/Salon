using Microsoft.AspNetCore.Mvc;
using Salon.BLL.Interfaces;
using Salon.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderManager _orderManager;
        private readonly IServiceManager _serviceManager;
        private readonly ICustomerManager _customerManager;

        public OrderController(IOrderManager orderManager, IServiceManager serviceManager, ICustomerManager customerManager)
        {
            _orderManager = orderManager;
            _serviceManager = serviceManager;
            _customerManager = customerManager;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            var orders = _orderManager.GetOrders(page);

            return View(orders);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            var order = _orderManager.GetOrder((int)id);
            return View(order);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var order = _orderManager.GetOrder((int)id);
            return View(order);
        }

        [HttpPost]
        public IActionResult Edit(OrderViewModel order)
        {
            _orderManager.UpdateOrder(order.Id, order);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            var customers = _customerManager.GetCustomers();
            var services = _serviceManager.GetServices();
            GlobalViewModel vm = new GlobalViewModel
            {
                Customer = customers.Customer,
                Service = services.Service
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(GlobalViewModel orderToCreate)
        {
            _orderManager.AddOrder(orderToCreate);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Delete(int? id)
        {
            var order = _orderManager.GetOrder((int)id);
            return View(order);
        }

        [HttpPost]
        public IActionResult Delete(OrderViewModel order)
        {
            var deletedOrder = _orderManager.DeleteOrder(order.Id);
            return RedirectToAction("Index");
        }
    }
}
