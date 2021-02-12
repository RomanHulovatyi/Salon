using Microsoft.AspNetCore.Mvc;
using Salon.BLL.Interfaces;
using Salon.BLL.ViewModels;

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
            var services = _serviceManager.GetServices(page);

            return View(services);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            var service = _serviceManager.GetService((int)id);
            return View(service);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var service = _serviceManager.GetService((int)id);
            return View(service);
        }

        [HttpPost]
        public IActionResult Edit(ServiceViewModel service)
        {

            var updatedServcie = _serviceManager.UpdateService(service.Id, service);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ServiceViewModel service)
        {
            _serviceManager.AddService(service);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Delete(int? id)
        {
            var service = _serviceManager.GetService((int)id);
            return View(service);
        }

        [HttpPost]
        public IActionResult Delete(ServiceViewModel service)
        {
            var deletedCustomer = _serviceManager.DeleteService(service.Id);
            return RedirectToAction("Index");
        }
    }
}
