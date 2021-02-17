using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL;
using Salon.Entities.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Salon.Validation
{
    public class PhoneUniqueness : IUniqueness
    {
        public List<string> CheckList { get; set; }
        private ISalonRepository<CustomerEntity> _salonManager;

        public PhoneUniqueness(ISalonRepository<CustomerEntity> salonManager)
        {
            _salonManager = salonManager;
        }

        public bool IsUnique(object value)
        {
            string v = value.ToString();
            CheckList = (List<string>)_salonManager.GetPhoneNumbers();

            return !CheckList.Contains(v);
        }
    }
}
