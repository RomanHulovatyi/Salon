using Salon.Abstractions.Interfaces;
using Salon.BLL.Interfaces;
using Salon.BLL.ViewModels;
using Salon.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Salon.BLL.Services
{
    public class ServiceManager : IServiceManager
    {
        private ISalonManager<Service> _salonManager;

        public ServiceManager(ISalonManager<Service> salonManager)
        {
            _salonManager = salonManager;
        }

        public void AddService(ServiceViewModel service)
        {
            try
            {
                Service newServie = new Service
                {
                    NameOfService = service.NameOfService,
                    Price = service.Price
                };

                Service createdService = _salonManager.Add(newServie);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
                
        }

        public string DeleteService(int id)
        {
            try
            {
                IEnumerable<int> listOfIds = _salonManager.GetIds();

                if (listOfIds.Contains(id))
                {
                    _salonManager.Delete(id);
                    return $"Service with id {id} deleted";
                }
                else
                {
                    throw new Exception($"Service with id {id} doesen't found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ServiceViewModel GetService(int id)
        {
            try
            {
                IEnumerable<int> listOfIds = _salonManager.GetIds();

                if (listOfIds.Contains(id))
                {
                    Service selectedService = _salonManager.GetSingle(id);

                    ServiceViewModel serviceViewModel = new ServiceViewModel
                    {
                        Id = selectedService.Id,
                        NameOfService = selectedService.NameOfService,
                        Price = selectedService.Price
                    };

                    return serviceViewModel;
                }
                else
                {
                    throw new Exception($"Service with id {id} doesen't found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ServiceIndexViewModel GetServices(int page = 1)
        {
            try
            {
                IEnumerable<Service> customers = _salonManager.GetList();

                var count = customers.Count();

                int pageSize = 8;

                var items = customers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                List<ServiceViewModel> servicesVM = new List<ServiceViewModel>();
                foreach (Service c in items)
                {
                    servicesVM.Add(new ServiceViewModel
                    {
                        Id = c.Id,
                        NameOfService = c.NameOfService,
                        Price = c.Price
                    });
                }

                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                ServiceIndexViewModel viewModel = new ServiceIndexViewModel
                {
                    PageViewModel = pageViewModel,
                    Service = servicesVM
                };

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ServiceIndexViewModel GetServices()
        {
            try
            {
                IEnumerable<Service> customers = _salonManager.GetList();

                List<ServiceViewModel> servicesVM = new List<ServiceViewModel>();
                foreach (Service c in customers)
                {
                    servicesVM.Add(new ServiceViewModel
                    {
                        Id = c.Id,
                        NameOfService = c.NameOfService,
                        Price = c.Price
                    });
                }

                ServiceIndexViewModel viewModel = new ServiceIndexViewModel
                {
                    Service = servicesVM.OrderBy(x => x.NameOfService)
                };

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ServiceViewModel UpdateService(int id, ServiceViewModel service)
        {
            try
            {
                Service serviceSelected = _salonManager.GetSingle(id);

                Service serviceToUpdate = new Service
                {
                    NameOfService = service.NameOfService,
                    Price = service.Price
               };

               Service updatedService = _salonManager.Update(id, serviceToUpdate);


               ServiceViewModel serviceViewModel = new ServiceViewModel
               {
                   NameOfService = updatedService.NameOfService,
                   Price = updatedService.Price
               };

               return serviceViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
