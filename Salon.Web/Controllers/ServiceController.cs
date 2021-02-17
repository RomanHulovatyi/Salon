using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Salon.BLL.Interfaces;
using Salon.BLL.ViewModels;
using Salon.Web.Models;
using System.Collections.Generic;

namespace Salon.Web.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public ServiceController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            var services = _serviceManager.Get(page);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<ServiceModel, ServiceViewModel>());
            var mapper = new Mapper(config);
            var servicesVM = mapper.Map<List<ServiceViewModel>>(services.Service);

            var indexVM = new ServiceIndexViewModel
            {
                Service = servicesVM,
                PageViewModel = services.PageViewModel
            };

            return View(indexVM);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            var service = _serviceManager.GetService((int)id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<ServiceModel, ServiceViewModel>());
            var mapper = new Mapper(config);
            var serviceVM = mapper.Map<ServiceViewModel>(service);

            return View(serviceVM);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var service = _serviceManager.GetService((int)id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<ServiceModel, ServiceViewModel>());
            var mapper = new Mapper(config);
            var serviceVM = mapper.Map<ServiceViewModel>(service);

            return View(serviceVM);
        }

        [HttpPost]
        public IActionResult Edit(ServiceModel service)
        {

            var updatedServcie = _serviceManager.Update(service.Id, service);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ServiceModel service)
        {
            _serviceManager.Add(service);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Delete(int? id)
        {
            var service = _serviceManager.GetService((int)id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<ServiceModel, ServiceViewModel>());
            var mapper = new Mapper(config);
            var serviceVM = mapper.Map<ServiceViewModel>(service);

            return View(serviceVM);
        }

        [HttpPost]
        public IActionResult Delete(ServiceModel service)
        {
            var deletedCustomer = _serviceManager.Delete(service.Id);
            return RedirectToAction("Index");
        }
    }
}
