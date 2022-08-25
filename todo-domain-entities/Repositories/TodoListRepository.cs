using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using todo_domain_entities.EF;
using todo_domain_entities.Entities;
using todo_domain_entities.Interfaces;

namespace todo_domain_entities.Repositories
{
    public class TodoListRepository : IRepository<TodoList>
    {
        private readonly TodoDBContext _context;

        public TodoListRepository(TodoDBContext context)
        {
            _context = context;
        }

        public IEnumerable<TodoList> ReadAll()
        {
            return _context.TodoLists;
        }

        public TodoList Read(int id)
        {
            return _context.TodoLists.Find(id);
        }

        public void Create(TodoList item)
        {
            _context.TodoLists.Add(item);
        }

        public void Update(TodoList item)
        {
            var existingList = _context.TodoLists.Local.SingleOrDefault(l => l.Id == item.Id);
            if (existingList != null)
                _context.Entry(existingList).State = EntityState.Detached;

            _context.TodoLists.Update(item);
        }

        public void Delete(int id)
        {
            TodoList item = _context.TodoLists.Find(id);
            if (item != null)
            {
                _context.TodoLists.Remove(item);
                // TODO: Delete items form TodoItems
            }
        }
    }
}
