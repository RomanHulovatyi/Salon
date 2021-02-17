using System.Collections.Generic;

namespace Salon.BLL.ViewModels
{
    public class CustomerIndexModel
    {
        public IEnumerable<CustomerModel> Customer { get; set; }
        public PageModel PageViewModel { get; set; }
    }
}
