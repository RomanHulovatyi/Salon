using Salon.Abstractions.Interfaces;
using Salon.BLL.Interfaces;
using Salon.BLL.ViewModels;
using Salon.Entities.Models;
using System;
using System.Collections.Generic;

namespace Salon.BLL.Services
{
    public class StateManager : IStateManager
    {
        private ISalonManager<State> _salonManager;

        public StateManager(ISalonManager<State> salonManager)
        {
            _salonManager = salonManager;
        }

        public IEnumerable<StateViewModel> GetStates()
        {
            try
            {
                IEnumerable<State> customers = _salonManager.GetList();

                List<StateViewModel> statesVM = new List<StateViewModel>();
                foreach (State c in customers)
                {
                    statesVM.Add(new StateViewModel
                    {
                        Id = c.Id,
                        OrderStatus = c.OrderStatus
                    });
                }

                return statesVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
