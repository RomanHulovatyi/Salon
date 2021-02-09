using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.BLL.ViewModels
{
    public class ServiceIndexViewModel
    {
        public IEnumerable<ServiceViewModel> Service { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}

