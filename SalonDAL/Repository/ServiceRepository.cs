using Salon.Abstractions.Interfaces;
using SalonDAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalonEf
{
    public class ServiceRepository 
    {
        private readonly SalonContext _context;

        public ServiceRepository(SalonContext context)
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
            var service = _context.Services.SingleOrDefault(x => x.Id == id);

            _context.Services.Remove(service);
            _context.SaveChanges();
        }

        public IEnumerable<Service> GetList()
        {
            return _context.Services.ToList();
        }

        public Service GetSingle(int id)
        {
            return _context.Services.SingleOrDefault(x => x.Id == id);
        }

        public Service Update(int id, Service service)
        {
            var serviceToUpdate = _context.Services.SingleOrDefault(x => x.Id == id);

            serviceToUpdate.NameOfService = service.NameOfService;
            serviceToUpdate.Price = service.Price;

            _context.Services.Update(serviceToUpdate);
            _context.SaveChanges();

            return serviceToUpdate;
        }

        public List<int> GetIds()
        {
            return _context.Services.Select(x => x.Id).ToList();
        }
    }
}
