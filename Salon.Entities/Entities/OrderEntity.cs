using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Entities.Models
{
    public class OrderEntity : Table
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateOfProcedure { get; set; }
        public int StatusId { get; set; }
    }
}
