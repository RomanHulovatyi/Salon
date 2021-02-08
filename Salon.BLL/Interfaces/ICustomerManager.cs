using Salon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.BLL.Interfaces
{
    public interface ICustomerManager
    {
        public DTO.Customer AddCustomer(DTO.Customer customer);
        public IEnumerable<DTO.Customer> GetCustomers();
        public DTO.Customer GetCustomer(int id);
        public DTO.Customer UpdateCustomer(int id, DTO.Customer customer);
        public string DeleteCustomer(int id);
    }
}
