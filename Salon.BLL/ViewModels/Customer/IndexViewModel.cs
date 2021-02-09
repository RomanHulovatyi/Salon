using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.BLL.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<CustomerViewModel> Customer { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
