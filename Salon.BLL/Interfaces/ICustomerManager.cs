using Salon.BLL.ViewModels;

namespace Salon.BLL.Interfaces
{
    public interface ICustomerManager
    {
        public CustomerModel Add(CustomerModel customer);
        public CustomerIndexModel Get(int page);
        public CustomerIndexModel Get();
        public CustomerModel GetCustomer(int id);
        public CustomerModel Update(int id, CustomerModel customer);
        public string Delete(int id);
    }
}
