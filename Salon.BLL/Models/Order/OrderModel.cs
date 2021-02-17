using System;

namespace Salon.BLL.ViewModels
{
    public class OrderModel
    {
        public int Id { get; set; }
        public CustomerModel Customer { get; set; }
        public ServiceModel Service { get; set; }
        public decimal? Price { get; set; }
        public DateTime Date { get; set; }
        public StateModel Status { get; set; }
    }
}
