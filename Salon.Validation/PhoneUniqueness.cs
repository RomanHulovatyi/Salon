using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL;
using Salon.Entities.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Salon.Validation
{
    public class PhoneUniqueness : IUniqueness
    {
        public List<string> ColumnName { get; set; }
        private ISalonManager<Customer> _salonManager;

        public PhoneUniqueness(ISalonManager<Customer> salonManager)
        {
            _salonManager = salonManager;
        }

        public bool IsUnique(object value)
        {
            string v = value.ToString();
            ColumnName = (List<string>)_salonManager.GetPhoneNumbers();

            return !ColumnName.Contains(v);
        }
    }
}
