using Salon.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.BLL.Interfaces
{
    public interface IOrderManager
    {
        public void AddOrder(GlobalViewModel order);
        public OrderIndexViewModel GetOrders(int page);
        public OrderViewModel GetOrder(int id);
        public void UpdateOrder(int id, OrderViewModel order);
        public string DeleteOrder(int id);
    }
}
