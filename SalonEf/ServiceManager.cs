using SalonDAL.Models;
using SalonDAL.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SalonEf
{
    public class ServiceManager : ISalonManager<Service>
    {
        private readonly SalonContext _context;

        public ServiceManager(SalonContext context)
        {
            _context = context;
        }

        public Service Add(Service service)
        {
            Service newService = new Service
            {
                NameOfService = service.NameOfService,
                Price = service.Price
            };

            _context.Services.Add(newService);
            _context.SaveChanges();
            return newService;
        }

        public void Delete(int id)
        {
            var service = _context.Services.Single(x => x.Id == id);

            _context.Services.Remove(service);
            _context.SaveChanges();
        }

        public IEnumerable<Service> GetList()
        {
            IEnumerable<Service> listOfCustomers = _context.Services.ToList();
            return listOfCustomers;
        }

        public Service GetSingle(int id)
        {
            var service = _context.Services.Single(x => x.Id == id);
            return service;
        }

        public Service Update(int id, Service service)
        {
            var serviceToUpdate = _context.Services.Single(x => x.Id == id);

            serviceToUpdate.NameOfService = service.NameOfService;
            serviceToUpdate.Price = service.Price;

            _context.Services.Update(serviceToUpdate);
            _context.SaveChanges();

            return serviceToUpdate;
        }
    }
}
