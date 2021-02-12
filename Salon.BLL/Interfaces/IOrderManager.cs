using Salon.BLL.ViewModels;

namespace Salon.BLL.Interfaces
{
    public interface IOrderManager
    {
        public void AddOrder(GlobalViewModel order);
        public OrderIndexViewModel GetOrders(int page);
        public OrderViewModel GetOrder(int id);
        public void UpdateOrder(int id, GlobalViewModel order);
        public string DeleteOrder(int id);
    }
}
