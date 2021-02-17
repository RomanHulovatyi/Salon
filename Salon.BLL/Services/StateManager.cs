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
        private ISalonRepository<StateEntity> _salonManager;

        public StateManager(ISalonRepository<StateEntity> salonManager)
        {
            _salonManager = salonManager;
        }

        public IEnumerable<StateModel> GetStates()
        {
            try
            {
                IEnumerable<StateEntity> customers = _salonManager.GetList();

                List<StateModel> statesVM = new List<StateModel>();
                foreach (StateEntity c in customers)
                {
                    statesVM.Add(new StateModel
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
