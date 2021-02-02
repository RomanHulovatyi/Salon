using System;
using System.Collections.Generic;

#nullable disable

namespace SalonDAL.Models
{
    public partial class State : Table
    {
        public State()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string OrderStatus { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
