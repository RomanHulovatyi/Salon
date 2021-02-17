using Salon.Entities.Models;
using System.Collections.Generic;

namespace Salon.Abstractions.Interfaces
{
    public interface ISalonRepository<T> where T : Table
    {
        T Add(T entity);
        void Delete(int id);
        IEnumerable<T> GetList();
        T Update(int id, T entity);
        T GetSingle(int id);
        IEnumerable<string> GetPhoneNumbers();
        IEnumerable<string> GetEmails();
        List<int> GetIds();
    }
}
