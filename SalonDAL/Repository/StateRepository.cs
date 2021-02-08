using Salon.Abstractions.Interfaces;
using SalonDAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalonEf
{
    public class StateRepository 
    {
        private readonly SalonContext _context;

        public StateRepository(SalonContext context)
        {
            _context = context;
        }

        public State Add(State state)
        {
            State newState = new State
            {
                OrderStatus = state.OrderStatus
            };

            _context.States.Add(newState);
            _context.SaveChanges();

            return newState;
        }

        public void Delete(int id)
        {
            var state = _context.States.SingleOrDefault(x => x.Id == id);

            _context.States.Remove(state);
            _context.SaveChanges();
        }

        public IEnumerable<State> GetList()
        {
            return _context.States.ToList();
        }

        public State GetSingle(int id)
        {
            return _context.States.SingleOrDefault(x => x.Id == id); ;
        }

        public State Update(int id, State state)
        {
            var stateToUpdate = _context.States.SingleOrDefault(x => x.Id == id);

            stateToUpdate.OrderStatus = state.OrderStatus;

            _context.States.Update(stateToUpdate);
            _context.SaveChanges();

            return stateToUpdate;
        }

        public List<int> GetIds()
        {
            return _context.States.Select(x => x.Id).ToList();
        }
    }
}
