using Salon.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Salon.Web.Models
{
    public class GlobalViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public int StatusId { get; set; }
        public IEnumerable<CustomerModel> Customer { get; set; }
        public IEnumerable<ServiceModel> Service { get; set; }
        public IEnumerable<StateModel> State { get; set; }

    }
}
