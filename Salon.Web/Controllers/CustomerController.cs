using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Salon.BLL.Interfaces;
using Salon.BLL.ViewModels;
using Salon.Web.Models;
using System.Collections.Generic;

namespace Salon.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerManager _customerManager;

        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            var customers = _customerManager.Get(page);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CustomerModel, CustomerViewModel>());
            var mapper = new Mapper(config);
            var customersVM = mapper.Map<List<CustomerViewModel>>(customers.Customer);

            var indexVM = new IndexViewModel
            {
                Customer = customersVM,
                PageViewModel = customers.PageViewModel
            };

            return View(indexVM);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            var customer = _customerManager.GetCustomer((int)id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CustomerModel, CustomerViewModel>());
            var mapper = new Mapper(config);
            var customerVM = mapper.Map<CustomerViewModel>(customer);

            return View(customerVM);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var customer = _customerManager.GetCustomer((int)id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CustomerModel, CustomerViewModel>());
            var mapper = new Mapper(config);
            var customerVM = mapper.Map<CustomerViewModel>(customer);

            return View(customerVM);
        }

        [HttpPost]
        public IActionResult Edit(CustomerModel customer)
        {
            var updatedCustomer = _customerManager.Update(customer.Id, customer);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerModel customer)
        {
            if (ModelState.IsValid)
            {
                var createdCustomer = _customerManager.Add(customer);
                return RedirectToAction("Index");
            }
                
            return Content("This phone number or email already taken");
        }

        [HttpGet]
        public ViewResult Delete(int? id)
        {
            var customer = _customerManager.GetCustomer((int)id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CustomerModel, CustomerViewModel>());
            var mapper = new Mapper(config);
            var customerVM = mapper.Map<CustomerViewModel>(customer);

            return View(customerVM);
        }

        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteCustomer(int? id)
        {
            var result = _customerManager.Delete((int)id);
            return RedirectToAction("Index");
        }
    }
}
