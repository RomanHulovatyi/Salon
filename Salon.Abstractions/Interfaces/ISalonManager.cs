using Salon.Entities.Models;
using System.Collections.Generic;

namespace Salon.Abstractions.Interfaces
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
