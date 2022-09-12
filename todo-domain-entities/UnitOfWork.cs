using System;
using todo_domain_entities.EF;
using todo_domain_entities.Entities;
using todo_domain_entities.Interfaces;
using todo_domain_entities.Repositories;

namespace todo_domain_entities
{
    /// <summary>
    /// The class represent unit of work for BL.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private TodoListRepository _todoListRepository;
        private TodoItemRepository _todoItemRepository;
        private UserRepository _userRepository;

        private TodoDBContext DataBase { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        public UnitOfWork()
        {
            DataBase = new TodoDBContext();
        }

        /// <inheritdoc/>
        public IRepository<TodoList> TodoLists
        {
            get
            {
                _todoListRepository ??= new TodoListRepository(DataBase);

                return _todoListRepository;
            }
        }

        /// <inheritdoc/>
        public IRepository<TodoItem> TodoItems
        {
            get
            {
                _todoItemRepository ??= new TodoItemRepository(DataBase);

                return _todoItemRepository;
            }
        }

        /// <inheritdoc/>
        public IRepository<User> Users
        {
            get
            {
                _userRepository ??= new UserRepository(DataBase);

                return _userRepository;
            }
        }

        /// <inheritdoc/>
        public void Save()
        {
            DataBase.SaveChanges();
        }

        /// <summary>
        /// The method to dispose this.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The method to dispose this.
        /// </summary>
        /// <param name="disposing">Whether or not to start freeing this object's memory.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
        }
    }
}
