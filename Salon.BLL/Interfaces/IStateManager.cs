using Salon.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.BLL.Interfaces
{
    public interface IStateManager
    {
        public IEnumerable<StateViewModel> GetStates();
    }
}
