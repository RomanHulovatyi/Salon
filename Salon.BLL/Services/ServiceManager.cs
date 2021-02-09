using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL;
using Salon.BLL.Interfaces;
using Salon.BLL.ViewModels;
using Salon.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.BLL.Services
{
    public class ServiceManager : IServiceManager
    {
        public void AddService(ServiceViewModel service)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Service> salon = new ServiceRepository(connection);

                    Service newServie = new Service
                    {
                        NameOfService = service.NameOfService,
                        Price = service.Price
                    };

                    Service createdService = salon.Add(newServie);

                    //ServiceViewModel serviceViewModel = new ServiceViewModel
                    //{
                    //    Id = createdService.Id,
                    //    NameOfService = createdService.NameOfService,
                    //    Price = createdService.Price
                    //};

                    //return serviceViewModel;
                }
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
                using (SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Service> salon = new ServiceRepository(connection);

                    ServiceRepository serviceRepository = new ServiceRepository(connection);
                    IEnumerable<int> listOfIds = serviceRepository.GetIds();

                    if (listOfIds.Contains(id))
                    {
                        salon.Delete(id);
                        return $"Service with id {id} deleted";
                    }
                    else
                    {
                        throw new Exception($"Service with id {id} doesen't found");
                    }
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
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Service> salon = new ServiceRepository(connection);

                    ServiceRepository serviceRepository = new ServiceRepository(connection);
                    IEnumerable<int> listOfIds = serviceRepository.GetIds();

                    if (listOfIds.Contains(id))
                    {
                        Service selectedService = salon.GetSingle(id);

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
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Service> salon = new ServiceRepository(connection);

                    IEnumerable<Service> customers = salon.GetList();

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
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<Service> salon = new ServiceRepository(connection);
                    Service serviceSelected = salon.GetSingle(id);

                    Service serviceToUpdate = new Service
                    {
                        NameOfService = service.NameOfService,
                        Price = service.Price
                    };

                    Service updatedService = salon.Update(id, serviceToUpdate);


                    ServiceViewModel serviceViewModel = new ServiceViewModel
                    {
                        NameOfService = updatedService.NameOfService,
                        Price = updatedService.Price
                    };

                    return serviceViewModel;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
