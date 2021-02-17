using System.Collections.Generic;

namespace Salon.BLL.ViewModels
{
    public class ServiceIndexModel
    {
        public IEnumerable<ServiceModel> Service { get; set; }
        public PageModel PageViewModel { get; set; }
    }
}

