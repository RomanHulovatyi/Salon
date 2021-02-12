using System.Collections.Generic;

namespace Salon.BLL.ViewModels
{
    public class OrderIndexViewModel
    {
        public IEnumerable<OrderViewModel> Order { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
