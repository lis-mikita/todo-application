using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using todo_domain_entities.Entities;

namespace todo_domain_entities.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> ReadAll();
        T Read(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
