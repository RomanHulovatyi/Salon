using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL;
using Salon.ADO.DAL.Connection;
using Salon.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Validation
{
    public class EmailUnique : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            ISqlConnectionFactory sql = new SqlConnectionFactory();
            ISalonRepository<CustomerEntity> manager = new CustomerRepository(sql);
            IUniqueness uniqueness = new EmailUniqueness(manager);
            return uniqueness.IsUnique(value);
        }
    }
}
