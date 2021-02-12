using System;

namespace Salon.BLL.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public CustomerViewModel Customer { get; set; }
        public ServiceViewModel Service { get; set; }
        public decimal? Price { get; set; }
        public DateTime Date { get; set; }
        public StateViewModel Status { get; set; }
    }
}
