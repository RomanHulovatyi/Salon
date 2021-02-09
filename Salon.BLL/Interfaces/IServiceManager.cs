using Salon.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.BLL.Interfaces
{
    public interface IServiceManager
    {
        public void AddService(ServiceViewModel customer);
        public ServiceIndexViewModel GetServices(int page);
        public ServiceIndexViewModel GetServices();
        public ServiceViewModel GetService(int id);
        public ServiceViewModel UpdateService(int id, ServiceViewModel customer);
        public string DeleteService(int id);
    }
}
