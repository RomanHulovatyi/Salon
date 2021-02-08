
using SalonDAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalonEf
{
    public class CustomerRepository
    {
        private readonly SalonContext _context;

        public CustomerRepository(SalonContext context)
        {
            _context = context;
        }

        public Customer Add(Customer customer)
        {
            Customer newCustomer = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email
            };
            
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();

            return newCustomer;
        }

        public void Delete(int id)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Id == id);

            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

        public IEnumerable<Customer> GetList()
        {
            return _context.Customers.ToList();
        }

        public Customer Update(int id, Customer customer)
        {
            var customerToUpdate = _context.Customers.SingleOrDefault(x => x.Id == id);

            customerToUpdate.FirstName = customer.FirstName;
            customerToUpdate.LastName = customer.LastName;
            customerToUpdate.PhoneNumber = customer.PhoneNumber;
            customerToUpdate.Email = customer.Email;

            _context.Customers.Update(customerToUpdate);
            _context.SaveChanges();

            return customerToUpdate;
        }

        public Customer GetSingle(int id)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Id == id);
            return customer;
        }

        public List<string> GetPhoneNumbers()
        {
            return _context.Customers.Select(x => x.PhoneNumber).ToList();
        }

        public List<string> GetEmails()
        {
            return _context.Customers.Select(x => x.Email).ToList();
        }

        public List<int> GetIds()
        {
            return _context.Customers.Select(x => x.Id).ToList();
        }
    }
}
