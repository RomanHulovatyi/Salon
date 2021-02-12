using System.Collections.Generic;

namespace Salon.BLL.ViewModels
{
    public class ServiceIndexViewModel
    {
        public IEnumerable<ServiceViewModel> Service { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}

