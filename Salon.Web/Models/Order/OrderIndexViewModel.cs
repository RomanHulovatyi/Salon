using Salon.BLL.ViewModels;
using System.Collections.Generic;

namespace Salon.Web.Models
{
    public class OrderIndexViewModel
    {
        public IEnumerable<OrderViewModel> Order { get; set; }
        public PageModel PageViewModel { get; set; }
    }
}
