using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using todo_domain_entities.EF;
using todo_domain_entities.Entities;
using todo_domain_entities.Interfaces;

namespace todo_domain_entities.Repositories
{
    /// <summary>
    /// This class provides an action with an object of User.
    /// </summary>
    public class UserRepository : IRepository<User>
    {
        private readonly TodoDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">Object for session with database.</param>
        public UserRepository(TodoDBContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public IEnumerable<User> ReadAll()
        {
            return _context.Users;
        }

        /// <inheritdoc/>
        public User Read(int id)
        {
            return _context.Users.Find(id);
        }

        /// <inheritdoc/>
        public void Create(User item)
        {
            _context.Users.Add(item);
        }

        /// <inheritdoc/>
        public void Update(User item)
        {
            var existingList = _context.Users.Local.SingleOrDefault(l => l.Id == item.Id);
            if (existingList != null)
            {
                _context.Entry(existingList).State = EntityState.Detached;
            }

            _context.Users.Update(item);
        }

        /// <inheritdoc/>
        public void Delete(int id)
        {
            User user = _context.Users.Find(id);
            if (user != null)
            {
                foreach (var list in _context.TodoLists.Where(x => x.UserId == id))
                {
                    foreach (var item in _context.TodoItems.Where(x => x.TodoListId == list.Id))
                    {
                        _context.TodoItems.Remove(item);
                    }

                    _context.TodoLists.Remove(list);
                }

                _context.Users.Remove(user);
            }
        }
    }
}
