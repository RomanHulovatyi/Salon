using Salon.Abstractions.Interfaces;
using Salon.Entities.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Salon.Validation
{
    public class EmailUniqueness : IUniqueness
    {
        public List<string> CheckList { get; set; }

        private ISalonRepository<CustomerEntity> _salonManager;

        public EmailUniqueness(ISalonRepository<CustomerEntity> salonManager)
        {
            _salonManager = salonManager;
        }

        public bool IsUnique(object value)
        {
            string v = value.ToString();

            CheckList = (List<string>)_salonManager.GetEmails();

            return !CheckList.Contains(v);
        }
    }
}
