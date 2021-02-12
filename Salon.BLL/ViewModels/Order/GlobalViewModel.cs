using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Salon.BLL.ViewModels
{
    public class GlobalViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public int StatusId { get; set; }
        public IEnumerable<CustomerViewModel> Customer { get; set; }
        public IEnumerable<ServiceViewModel> Service { get; set; }
        public IEnumerable<StateViewModel> State { get; set; }

    }
}
