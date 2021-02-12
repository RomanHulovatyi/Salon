using Salon.BLL.ViewModels;

namespace Salon.BLL.Interfaces
{
    public interface ICustomerManager
    {
        public CustomerViewModel AddCustomer(CustomerViewModel customer);
        public IndexViewModel GetCustomers(int page);
        public IndexViewModel GetCustomers();
        public CustomerViewModel GetCustomer(int id);
        public CustomerViewModel UpdateCustomer(int id, CustomerViewModel customer);
        public string DeleteCustomer(int id);
    }
}
