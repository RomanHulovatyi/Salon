using Microsoft.AspNetCore.Mvc;
using Salon.BLL.Interfaces;
//using Salon.Entities.Models;
using Salon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IActionResult Index()
        {
            IEnumerable<Salon.DTO.Customer> customers = _customerManager.GetCustomers();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            Salon.DTO.Customer customer = _customerManager.GetCustomer((int)id);
            return View(customer);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Salon.DTO.Customer customer = _customerManager.GetCustomer((int)id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            Customer customerToUpdate = new Customer 
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email
            };

            Salon.DTO.Customer updatedCustomer = _customerManager.UpdateCustomer(customer.Id, customerToUpdate);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            var createdCustomer = _customerManager.AddCustomer(customer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Delete(int? id)
        {
            var customer = _customerManager.GetCustomer((int)id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Salon.DTO.Customer customer)
        {
            string deletedCustomer = _customerManager.DeleteCustomer(customer.Id);
            return RedirectToAction("Index");
        }
    }
}
