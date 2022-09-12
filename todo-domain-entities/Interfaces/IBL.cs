using System;
using System.Collections.Generic;
using todo_domain_entities.EntitiesBL;

namespace todo_domain_entities.Interfaces
{
    /// <summary>
    /// Provide mechanism for read/add/find/update/remove objects of TodoListBL, TodoItemBL and UserBL types.
    /// </summary>
    public interface IBL : IDisposable
    {
        /// <summary>
        /// The method to adding TodoListBL.
        /// </summary>
        /// <param name="element">The list to add.</param>
        void AddTodoList(TodoListBL element);

        /// <summary>
        /// The method to adding TodoItemBL.
        /// </summary>
        /// <param name="element">The item to add.</param>
        void AddTodoItem(TodoItemBL element);

        /// <summary>
        /// The method to adding UserBL.
        /// </summary>
        /// <param name="element">The user to add.</param>
        void AddUser(UserBL element);

        /// <summary>
        /// The method to removing TodoListBL.
        /// </summary>
        /// <param name="id">List ID to delete.</param>
        void RemoveTodoList(int id);

        /// <summary>
        /// The method to removing TodoItemBL.
        /// </summary>
        /// <param name="id">Item ID to delete.</param>
        void RemoveTodoItem(int id);

        /// <summary>
        /// The method to removing UserBL.
        /// </summary>
        /// <param name="id">User ID to delete.</param>
        void RemoveUser(int id);

        /// <summary>
        /// The method to finding TodoListBL.
        /// </summary>
        /// <param name="id">List ID to find.</param>
        /// <returns>TodoListBL object or null.</returns>
        TodoListBL FindTodoList(int id);

        /// <summary>
        /// The method to finding TodoItemBL.
        /// </summary>
        /// <param name="id">Item ID to find.</param>
        /// <returns>TodoItemBL object or null.</returns>
        TodoItemBL FindTodoItem(int id);

        /// <summary>
        /// The method to finding UserBL.
        /// </summary>
        /// <param name="id">User ID to find.</param>
        /// <returns>UserBL object or null.</returns>
        UserBL FindUser(int id);

        /// <summary>
        /// The method to updating TodoListBL.
        /// </summary>
        /// <param name="element">The list to update.</param>
        void UpdateTodoList(TodoListBL element);

        /// <summary>
        /// The method to updating TodoItemBL.
        /// </summary>
        /// <param name="element">The item to update.</param>
        void UpdateTodoItem(TodoItemBL element);

        /// <summary>
        /// The method to updating UserBL.
        /// </summary>
        /// <param name="element">The user to update.</param>
        void UpdateUser(UserBL element);

        /// <summary>
        /// The method to returning all object of TodoListBL from DB.
        /// </summary>
        /// <returns>Set of TodoListBL.</returns>
        IEnumerable<TodoListBL> GetTodoLists();

        /// <summary>
        /// The method to returning all object of TodoItemBL from DB.
        /// </summary>
        /// <returns>Set of TodoItemBL.</returns>
        IEnumerable<TodoItemBL> GetTodoItems();

        /// <summary>
        /// The method to returning all object of UserBL from DB.
        /// </summary>
        /// <returns>Set of UserBL.</returns>
        IEnumerable<UserBL> GetUsers();
    }
}
