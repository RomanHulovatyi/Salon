using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Entities.Models
{
    public class OrderViewEntity
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string Service { get; set; }
        public decimal? Price { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
