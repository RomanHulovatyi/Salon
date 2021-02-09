﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Salon.BLL.Interfaces;
using Salon.BLL.ViewModels;
using Salon.Entities.Models;
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
        public IActionResult Index(int page = 1)
        {
            var customers = _customerManager.GetCustomers(page);
             
            return View(customers);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            var customer = _customerManager.GetCustomer((int)id);
            return View(customer);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var customer = _customerManager.GetCustomer((int)id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(CustomerViewModel customer)
        {
            var updatedCustomer = _customerManager.UpdateCustomer(customer.Id, customer);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerViewModel customer)
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
        public IActionResult Delete(CustomerViewModel customer)
        {
            var deletedCustomer = _customerManager.DeleteCustomer(customer.Id);
            return RedirectToAction("Index");
        }
    }
}