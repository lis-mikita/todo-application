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
    public class TodoItemRepository : IRepository<TodoItem>
    {
        private readonly TodoDBContext _context;

        public TodoItemRepository(TodoDBContext context)
        {
            _context = context;
        }

        public IEnumerable<TodoItem> ReadAll()
        {
            return _context.TodoItems;
        }

        public TodoItem Read(int id)
        {
            return _context.TodoItems.Find(id);
        }

        public void Create(TodoItem item)
        {
            _context.TodoItems.Add(item);
        }

        public void Update(TodoItem item)
        {
            var existingItem = _context.TodoItems.Local.SingleOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
                _context.Entry(existingItem).State = EntityState.Detached;

            _context.TodoItems.Update(item);
            //TODO:
        }

        public void Delete(int id)
        {
            TodoItem item = _context.TodoItems.Find(id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
            }
        }

    }
}
