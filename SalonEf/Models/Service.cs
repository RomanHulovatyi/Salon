using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Entities.Models
{
    public class Service : Table
    {
        public int Id { get; set; }
        public string NameOfService { get; set; }
        public decimal? Price { get; set; }
    }
}
