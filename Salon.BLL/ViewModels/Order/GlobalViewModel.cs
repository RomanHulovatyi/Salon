using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.BLL.ViewModels
{
    public class GlobalViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public DateTime Date { get; set; }
        public int StatusId { get; set; }
        public IEnumerable<CustomerViewModel> Customer { get; set; }
        public IEnumerable<ServiceViewModel> Service { get; set; }
        public IEnumerable<StateViewModel> State { get; set; }

    }
}
