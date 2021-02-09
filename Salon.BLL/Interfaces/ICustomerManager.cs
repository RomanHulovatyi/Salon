
using Salon.BLL.ViewModels;
using Salon.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
