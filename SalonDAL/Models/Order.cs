using System;

#nullable disable

namespace SalonDAL.Models
{
    public partial class Order : Table
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateOfProcedure { get; set; }
        public int StatusId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Service Service { get; set; }
        public virtual State Status { get; set; }
    }
}
