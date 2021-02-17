using Salon.BLL.ViewModels;

namespace Salon.BLL.Interfaces
{
    public interface IOrderManager
    {
        public void Add(GlobalModel order);
        public OrderIndexModel Get(int page);
        public OrderModel GetOrder(int id);
        public void Update(int id, GlobalModel order);
        public string Delete(int id);
    }
}
