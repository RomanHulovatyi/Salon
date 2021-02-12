﻿using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL;
using Salon.Entities.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Salon.Validation
{
    public class PhoneUniqueness : ValidationAttribute
    {
        List<string> phones { get; set; }
        private ISalonManager<Customer> _salonManager;

        public PhoneUniqueness(CustomerRepository salonManager)
        {
            _salonManager = salonManager;
        }

        public override bool IsValid(object value)
        {
            string v = value.ToString();
            phones = (List<string>)_salonManager.GetPhoneNumbers();

            return !phones.Contains(v);
        }
    }
}
