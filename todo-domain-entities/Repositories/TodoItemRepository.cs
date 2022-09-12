using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using todo_domain_entities.EF;
using todo_domain_entities.Entities;
using todo_domain_entities.Interfaces;

namespace todo_domain_entities.Repositories
{
    /// <summary>
    /// This class provides an action with an object of TodoItem.
    /// </summary>
    public class TodoItemRepository : IRepository<TodoItem>
    {
        private readonly TodoDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoItemRepository"/> class.
        /// </summary>
        /// <param name="context">Object for session with database.</param>
        public TodoItemRepository(TodoDBContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public IEnumerable<TodoItem> ReadAll()
        {
            return _context.TodoItems;
        }

        /// <inheritdoc/>
        public TodoItem Read(int id)
        {
            return _context.TodoItems.Find(id);
        }

        /// <inheritdoc/>
        public void Create(TodoItem item)
        {
            _context.TodoItems.Add(item);
        }

        /// <inheritdoc/>
        public void Update(TodoItem item)
        {
            var existingItem = _context.TodoItems.Local.SingleOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                _context.Entry(existingItem).State = EntityState.Detached;
            }

            _context.TodoItems.Update(item);
        }

        /// <inheritdoc/>
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
