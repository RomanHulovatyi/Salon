using Salon.BLL.ViewModels;

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
