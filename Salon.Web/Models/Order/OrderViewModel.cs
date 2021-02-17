using Salon.BLL.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace Salon.Web.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public CustomerModel Customer { get; set; }
        public ServiceModel Service { get; set; }
        public decimal? Price { get; set; }
        public DateTime Date { get; set; }
        public StateModel Status { get; set; }
    }
}
