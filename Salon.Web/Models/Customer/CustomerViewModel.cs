using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL;
using Salon.ADO.DAL.Connection;
using Salon.Entities.Models;
using Salon.Validation;
using System.ComponentModel.DataAnnotations;

namespace Salon.Web.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please fill in this field")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please fill in this field")]
        public string LastName { get; set; }

        [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$", ErrorMessage = "Invalid Phone Number")]       
        [PhoneEnique]
        [Required(ErrorMessage = "Please fill in this field")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        [EmailUnique]
        [Required(ErrorMessage = "Please fill in this field")]
        public string Email { get; set; }
    }
}
