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
        private ISalonRepository<ServiceEntity> _salonManager;

        public ServiceManager(ISalonRepository<ServiceEntity> salonManager)
        {
            _salonManager = salonManager;
        }

        public void Add(ServiceModel service)
        {
            try
            {
                ServiceEntity newServie = new ServiceEntity
                {
                    NameOfService = service.NameOfService,
                    Price = service.Price
                };

                ServiceEntity createdService = _salonManager.Add(newServie);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
                
        }

        public string Delete(int id)
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

        public ServiceModel GetService(int id)
        {
            try
            {
                IEnumerable<int> listOfIds = _salonManager.GetIds();

                if (listOfIds.Contains(id))
                {
                    ServiceEntity selectedService = _salonManager.GetSingle(id);

                    ServiceModel serviceViewModel = new ServiceModel
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

        public ServiceIndexModel Get(int page = 1)
        {
            try
            {
                IEnumerable<ServiceEntity> customers = _salonManager.GetList();

                var count = customers.Count();

                int pageSize = 8;

                var items = customers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                List<ServiceModel> servicesVM = new List<ServiceModel>();
                foreach (ServiceEntity c in items)
                {
                    servicesVM.Add(new ServiceModel
                    {
                        Id = c.Id,
                        NameOfService = c.NameOfService,
                        Price = c.Price
                    });
                }

                PageModel pageViewModel = new PageModel(count, page, pageSize);
                ServiceIndexModel viewModel = new ServiceIndexModel
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

        public ServiceIndexModel Get()
        {
            try
            {
                IEnumerable<ServiceEntity> customers = _salonManager.GetList();

                List<ServiceModel> servicesVM = new List<ServiceModel>();
                foreach (ServiceEntity c in customers)
                {
                    servicesVM.Add(new ServiceModel
                    {
                        Id = c.Id,
                        NameOfService = c.NameOfService,
                        Price = c.Price
                    });
                }

                ServiceIndexModel viewModel = new ServiceIndexModel
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

        public ServiceModel Update(int id, ServiceModel service)
        {
            try
            {
                ServiceEntity serviceSelected = _salonManager.GetSingle(id);

                ServiceEntity serviceToUpdate = new ServiceEntity
                {
                    NameOfService = service.NameOfService,
                    Price = service.Price
               };

               ServiceEntity updatedService = _salonManager.Update(id, serviceToUpdate);


               ServiceModel serviceViewModel = new ServiceModel
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
