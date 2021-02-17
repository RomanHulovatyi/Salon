using Salon.BLL.ViewModels;
using System.Collections.Generic;

namespace Salon.Web.Models
{
    public class ServiceIndexViewModel
    {
        public IEnumerable<ServiceViewModel> Service { get; set; }
        public PageModel PageViewModel { get; set; }
    }
}

