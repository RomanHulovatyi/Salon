using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Salon.Validation;

namespace Salon.DTO
{
    public class Customer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a valid name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a valid surname")]
        public string LastName { get; set; }
        //[PhoneUniqueness(ErrorMessage = "This number already taken. Please enter new one")]
        [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
    }
}
