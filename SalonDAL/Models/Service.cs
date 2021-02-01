using System.Collections.Generic;

#nullable disable

namespace SalonDAL.Models
{
    public partial class Service : Table
    {
        public Service()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string NameOfService { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
