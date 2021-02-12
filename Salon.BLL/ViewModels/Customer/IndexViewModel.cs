using System.Collections.Generic;

namespace Salon.BLL.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<CustomerViewModel> Customer { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
