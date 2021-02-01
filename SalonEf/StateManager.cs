using SalonDAL.Models;
using SalonDAL.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SalonEf
{
    public class StateManager : ISalonManager<State>
    {
        private readonly SalonContext _context;

        public StateManager(SalonContext context)
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
            var state = _context.States.Single(x => x.Id == id);

            _context.States.Remove(state);
            _context.SaveChanges();
        }

        public IEnumerable<State> GetList()
        {
            IEnumerable<State> listOfStates = _context.States.ToList();
            return listOfStates;
        }

        public State GetSingle(int id)
        {
            var state = _context.States.Single(x => x.Id == id);
            return state;
        }

        public State Update(int id, State state)
        {
            var stateToUpdate = _context.States.Single(x => x.Id == id);

            stateToUpdate.OrderStatus = state.OrderStatus;

            _context.States.Update(stateToUpdate);
            _context.SaveChanges();

            return stateToUpdate;
        }
    }
}
