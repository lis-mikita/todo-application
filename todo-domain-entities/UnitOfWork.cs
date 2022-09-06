using System;
using System.Collections.Generic;
using System.Text;
using todo_domain_entities.EF;
using todo_domain_entities.Entities;
using todo_domain_entities.Interfaces;
using todo_domain_entities.Repositories;

namespace todo_domain_entities
{
    public class UnitOfWork : IUnitOfWork
    {
        private TodoDBContext DataBase { get; }
        private TodoListRepository todoListRepository;
        private TodoItemRepository todoItemRepository;
        private UserRepository userRepository;

        public UnitOfWork()
        {
            DataBase = new TodoDBContext();
        }

        public IRepository<TodoList> TodoLists
        {
            get
            {
                todoListRepository ??= new TodoListRepository(DataBase);

                return todoListRepository;
            }
        }

        public IRepository<TodoItem> TodoItems
        {
            get
            {
                todoItemRepository ??= new TodoItemRepository(DataBase);

                return todoItemRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                userRepository ??= new UserRepository(DataBase);

                return userRepository;
            }
        }

        public void Save()
        {
            DataBase.SaveChanges();
        }

        public void Dispose()
        {
            DataBase.Dispose();
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
        }
    }
}
