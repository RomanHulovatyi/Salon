using Salon.Abstractions.Interfaces;
using SalonDAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalonEf
{
    public class OrderRepository 
    {
        private readonly SalonContext _context;

        public OrderRepository(SalonContext context)
        {
            _context = context;
        }

        public Order Add(Order order)
        {
            Order newOrder = new Order
            {
                ServiceId = order.ServiceId,
                CustomerId = order.CustomerId,
                DateOfProcedure = order.DateOfProcedure,
                StatusId = order.StatusId
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            return newOrder;
        }

        public void Delete(int id)
        {
            var order = _context.Orders.SingleOrDefault(x => x.Id == id);

            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetList()
        {
            return _context.Orders.ToList();
        }

        public Order GetSingle(int id)
        {
            var order = _context.Orders.SingleOrDefault(x => x.Id == id);
            return order;
        }

        public Order Update(int id, Order order)
        {
            var orderToUpdate = _context.Orders.SingleOrDefault(x => x.Id == id);

            orderToUpdate.ServiceId = order.ServiceId;
            orderToUpdate.CustomerId = order.CustomerId;
            orderToUpdate.DateOfProcedure = order.DateOfProcedure;
            orderToUpdate.StatusId = order.StatusId;

            _context.Orders.Update(orderToUpdate);
            _context.SaveChanges();

            return orderToUpdate;
        }

        public IEnumerable<OrderTable> GetView()
        {
            return _context.OrderTables.OrderByDescending(x => x.Date).ToList();
        }

        public List<int> GetIds()
        {
            return _context.Orders.Select(x => x.Id).ToList();
        }
    }
}
