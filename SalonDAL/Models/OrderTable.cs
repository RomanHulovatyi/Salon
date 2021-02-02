using System;
using System.Collections.Generic;

#nullable disable

namespace SalonDAL.Models
{
    public partial class OrderTable
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string Service { get; set; }
        public decimal? Price { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
