using System;
using System.ComponentModel.DataAnnotations;

namespace Salon.BLL.ViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a valid name")]
        public string NameOfService { get; set; }
        //[RegularExpression(@"^\d+\,\d{6}$", ErrorMessage = "Enter a valid price")]
        //[Range(0, 9999.99, ErrorMessage = "Enter a valid price")]
        //[DataType(DataType.Currency)]
        //[RegularExpression(@"^\d+\.\d{0,2}$")]
        //[DataType(DataType.Currency)]
        //[Column(TypeName = "decimal(18, 2)")]
        //[RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        [Range(typeof(decimal), "0,00", "10000,00")]
        public decimal Price { get; set; }
    }
}
