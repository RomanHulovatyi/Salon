using Salon.BLL.ViewModels;
using System.Collections.Generic;

namespace Salon.Web.Models
{
    public class IndexViewModel
    {
        public IEnumerable<CustomerViewModel> Customer { get; set; }
        public PageModel PageViewModel { get; set; }
    }
}
