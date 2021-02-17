using Salon.BLL.ViewModels;

namespace Salon.BLL.Interfaces
{
    public interface IServiceManager
    {
        public void Add(ServiceModel customer);
        public ServiceIndexModel Get(int page);
        public ServiceIndexModel Get();
        public ServiceModel GetService(int id);
        public ServiceModel Update(int id, ServiceModel customer);
        public string Delete(int id);
    }
}
