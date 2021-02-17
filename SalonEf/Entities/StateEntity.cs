using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Entities.Models
{
    public class StateEntity : Table
    {
        public int Id { get; set; }
        public string OrderStatus { get; set; }
    }
}
