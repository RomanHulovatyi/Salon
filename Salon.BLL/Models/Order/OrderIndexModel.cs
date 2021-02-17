using System.Collections.Generic;

namespace Salon.BLL.ViewModels
{
    public class OrderIndexModel
    {
        public IEnumerable<OrderModel> Order { get; set; }
        public PageModel PageViewModel { get; set; }
    }
}
