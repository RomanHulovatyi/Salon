using Salon.ADO.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Salon.Validation
{
    public class PhoneUniqueness : ValidationAttribute
    {
        List<string> phones;

        public override bool IsValid(object value)
        {
            using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
            {
                CustomerRepository customerRepository = new CustomerRepository(connection);
                phones = (List<string>)customerRepository.GetPhoneNumbers();

                if (!phones.Contains(value.ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        
    }
}
