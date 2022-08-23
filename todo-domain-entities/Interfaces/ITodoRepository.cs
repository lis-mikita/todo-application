using System;
using System.Collections.Generic;
using System.Text;
using todo_domain_entities.Entities;

namespace todo_domain_entities.Interfaces
{
    public interface ITodoRepository : IDisposable
    {
        IRepository<TodoList> TodoLists { get; }

        IRepository<TodoItem> TodoItems { get; }

        void Save();
    }
}
