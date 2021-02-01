using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonDAL.Models.Interfaces
{
    public interface ISalonManager<T> where T : Table
    {
        T Add(T entity);
        void Delete(int id);
        IEnumerable<T> GetList();
        T Update(int id, T entity);
        T GetSingle(int id);
    }
}
