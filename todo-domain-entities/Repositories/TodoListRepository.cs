using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using todo_domain_entities.EF;
using todo_domain_entities.Entities;
using todo_domain_entities.Interfaces;

namespace todo_domain_entities.Repositories
{
    /// <summary>
    /// This class provides an action with an object of TodoList.
    /// </summary>
    public class TodoListRepository : IRepository<TodoList>
    {
        private readonly TodoDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoListRepository"/> class.
        /// </summary>
        /// <param name="context">Object for session with database.</param>
        public TodoListRepository(TodoDBContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public IEnumerable<TodoList> ReadAll()
        {
            return _context.TodoLists;
        }

        /// <inheritdoc/>
        public TodoList Read(int id)
        {
            return _context.TodoLists.Find(id);
        }

        /// <inheritdoc/>
        public void Create(TodoList item)
        {
            _context.TodoLists.Add(item);
        }

        /// <inheritdoc/>
        public void Update(TodoList item)
        {
            var existingList = _context.TodoLists.Local.SingleOrDefault(l => l.Id == item.Id);
            if (existingList != null)
            {
                _context.Entry(existingList).State = EntityState.Detached;
            }

            _context.TodoLists.Update(item);
        }

        /// <inheritdoc/>
        public void Delete(int id)
        {
            TodoList list = _context.TodoLists.Find(id);
            if (list != null)
            {
                foreach (var item in _context.TodoItems.Where(x => x.TodoListId == id))
                {
                    _context.TodoItems.Remove(item);
                }

                _context.TodoLists.Remove(list);
            }
        }
    }
}
