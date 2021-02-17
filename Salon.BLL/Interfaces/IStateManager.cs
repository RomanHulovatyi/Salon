using Salon.BLL.ViewModels;
using System.Collections.Generic;

namespace Salon.BLL.Interfaces
{
    public interface IStateManager
    {
        public IEnumerable<StateModel> GetStates();
    }
}
