using SalonDAL.Models;
using SalonDAL.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SalonEf
{
    public class OrderManager : ISalonManager<Order>
    {
        private readonly SalonContext _context;

        public OrderManager(SalonContext context)
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
            var order = _context.Orders.Single(x => x.Id == id);

            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetList()
        {
            IEnumerable<Order> listOfOrders = _context.Orders.ToList();
            return listOfOrders;
        }

        public Order GetSingle(int id)
        {
            var order = _context.Orders.Single(x => x.Id == id);
            return order;
        }

        public Order Update(int id, Order order)
        {
            var orderToUpdate = _context.Orders.Single(x => x.Id == id);

            orderToUpdate.ServiceId = order.ServiceId;
            orderToUpdate.CustomerId = order.CustomerId;
            orderToUpdate.DateOfProcedure = order.DateOfProcedure;
            orderToUpdate.StatusId = order.StatusId;

            _context.Orders.Update(orderToUpdate);
            _context.SaveChanges();

            return orderToUpdate;
        }
    }
}
