using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.BLL.ViewModels
{
    public class OrderIndexViewModel
    {
        public IEnumerable<OrderViewModel> Order { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
