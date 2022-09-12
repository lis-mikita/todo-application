using System;
using todo_domain_entities.Entities;

namespace todo_domain_entities.Interfaces
{
    /// <summary>
    /// Provide unit of work for BL.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets mechanisms for action with TodoList objects.
        /// </summary>
        /// <value>
        /// Actions with TodoList.
        /// </value>
        IRepository<TodoList> TodoLists { get; }

        /// <summary>
        /// Gets mechanisms for action with TodoItem objects.
        /// </summary>
        /// <value>
        /// Actions with TodoItem.
        /// </value>
        IRepository<TodoItem> TodoItems { get; }

        /// <summary>
        /// Gets mechanisms for action with User objects.
        /// </summary>
        /// <value>
        /// Actions with User.
        /// </value>
        IRepository<User> Users { get; }

        /// <summary>
        /// The method provide save changes.
        /// </summary>
        void Save();
    }
}
