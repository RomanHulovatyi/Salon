using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL;
using Salon.ADO.DAL.Connection;
using Salon.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Validation
{
    public class PhoneEnique : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            ISqlConnectionFactory sql = new SqlConnectionFactory();
            ISalonManager<Customer> manager = new CustomerRepository(sql);
            IUniqueness uniqueness = new PhoneUniqueness(manager);
            return uniqueness.IsUnique(value);
        }
    }
}
