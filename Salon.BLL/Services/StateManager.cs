using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL;
using Salon.BLL.Interfaces;
using Salon.BLL.ViewModels;
using Salon.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.BLL.Services
{
    public class StateManager : IStateManager
    {
        public IEnumerable<StateViewModel> GetStates()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Salon; Integrated Security = True"))
                {
                    ISalonManager<State> salon = new StateRepository(connection);

                    IEnumerable<State> customers = salon.GetList();

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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
