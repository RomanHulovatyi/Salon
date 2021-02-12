﻿using Salon.Abstractions.Interfaces;
using Salon.Entities.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Salon.Validation
{
    public class EmailUniqueness : ValidationAttribute, IUniqueness
    {
        public List<string> ColumnName { get; set; }

        private ISalonManager<Customer> _salonManager;

        public EmailUniqueness(ISalonManager<Customer> salonManager)
        {
            _salonManager = salonManager;
        }

        public override bool IsValid(object value)
        {
            string v = value.ToString();

            ColumnName = (List<string>)_salonManager.GetEmails();

            return !ColumnName.Contains(v);
        }
    }
}
