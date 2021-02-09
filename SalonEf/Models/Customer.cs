﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Entities.Models
{
    public class Customer : Table
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a valid name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a valid surname")]
        public string LastName { get; set; }
        [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
    }
}