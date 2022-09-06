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
    public class UserRepository : IRepository<User>
    {
        private readonly TodoDBContext _context;

        public UserRepository(TodoDBContext context)
        {
            _context = context;
        }

        public IEnumerable<User> ReadAll()
        {
            return _context.Users;
        }

        public User Read(int id)
        {
            return _context.Users.Find(id);
        }

        public void Create(User item)
        {
            _context.Users.Add(item);
        }

        public void Update(User item)
        {
            var existingList = _context.Users.Local.SingleOrDefault(l => l.Id == item.Id);
            if (existingList != null)
                _context.Entry(existingList).State = EntityState.Detached;

            _context.Users.Update(item);
        }

        public void Delete(int id)
        {
            User user = _context.Users.Find(id);
            if (user != null)
            {
                foreach (var list in _context.TodoLists.Where(x => x.UserId == id))
                {
                    foreach (var item in _context.TodoItems.Where(x => x.ToDoListId == list.Id))
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
