using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL;
using Salon.ADO.DAL.Connection;
using Salon.Entities.Models;
using Salon.Validation;
using System.ComponentModel.DataAnnotations;

namespace Salon.BLL.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a valid name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a valid surname")]
        public string LastName { get; set; }

        [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$", ErrorMessage = "Invalid Phone Number")]       
        [PhoneEnique]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [EmailUnique]
        [Required(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
    }
}
