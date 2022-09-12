using System;
using System.Collections.Generic;
using AutoMapper;
using todo_domain_entities.Entities;
using todo_domain_entities.EntitiesBL;
using todo_domain_entities.Interfaces;

namespace todo_domain_entities
{
    /// <summary>
    /// The instance of BL(Business logic).
    /// </summary>
    public class BL : IBL
    {
        private IUnitOfWork DB { get; }

        private Mapper MapperList { get; }

        private Mapper MapperItem { get; }

        private Mapper MapperUser { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BL"/> class.
        /// </summary>
        /// <param name="unitOfWork">Transfers data between BL and DB.</param>
        public BL(IUnitOfWork unitOfWork)
        {
            DB = unitOfWork;

            var configList = new MapperConfiguration(cfg =>
                cfg.CreateMap<TodoList, TodoListBL>().ReverseMap());
            MapperList = new Mapper(configList);

            var configItem = new MapperConfiguration(cfg =>
                cfg.CreateMap<TodoItem, TodoItemBL>().ReverseMap());
            MapperItem = new Mapper(configItem);

            var configUser = new MapperConfiguration(cfg =>
                cfg.CreateMap<User, UserBL>().ReverseMap());
            MapperUser = new Mapper(configUser);
        }

        /// <inheritdoc/>
        public void AddTodoList(TodoListBL element)
        {
            if (element != null && element.Id == default)
            {
                DB.TodoLists.Create(MapperList.Map<TodoList>(element));
                DB.Save();
            }
        }

        /// <inheritdoc/>
        public void AddTodoItem(TodoItemBL element)
        {
            if (element != null && element.Id == default)
            {
                DB.TodoItems.Create(MapperItem.Map<TodoItem>(element));
                DB.Save();
            }
        }

        /// <inheritdoc/>
        public void AddUser(UserBL element)
        {
            if (element != null && element.Id == default)
            {
                DB.Users.Create(MapperUser.Map<User>(element));
                DB.Save();
            }
        }

        /// <inheritdoc/>
        public void RemoveTodoList(int id)
        {
            if (DB.TodoLists.Read(id) != null)
            {
                DB.TodoLists.Delete(id);
                DB.Save();
            }
        }

        /// <inheritdoc/>
        public void RemoveTodoItem(int id)
        {
            if (DB.TodoItems.Read(id) != null)
            {
                DB.TodoItems.Delete(id);
                DB.Save();
            }
        }

        /// <inheritdoc/>
        public void RemoveUser(int id)
        {
            if (DB.Users.Read(id) != null)
            {
                DB.Users.Delete(id);
                DB.Save();
            }
        }

        /// <inheritdoc/>
        public TodoListBL FindTodoList(int id)
        {
            return MapperList.Map<TodoListBL>(DB.TodoLists.Read(id));
        }

        /// <inheritdoc/>
        public TodoItemBL FindTodoItem(int id)
        {
            return MapperItem.Map<TodoItemBL>(DB.TodoItems.Read(id));
        }

        /// <inheritdoc/>
        public UserBL FindUser(int id)
        {
            return MapperUser.Map<UserBL>(DB.Users.Read(id));
        }

        /// <inheritdoc/>
        public void UpdateTodoList(TodoListBL element)
        {
            if (element != null)
            {
                TodoList toUpdate = MapperList.Map<TodoList>(element);

                if (DB.TodoLists.Read(element.Id) != null)
                {
                    DB.TodoLists.Update(toUpdate);
                    DB.Save();
                }
            }
        }

        /// <inheritdoc/>
        public void UpdateTodoItem(TodoItemBL element)
        {
            if (element != null)
            {
                TodoItem toUpdate = MapperItem.Map<TodoItem>(element);

                if (DB.TodoItems.Read(element.Id) != null)
                {
                    DB.TodoItems.Update(toUpdate);
                    DB.Save();
                }
            }
        }

        /// <inheritdoc/>
        public void UpdateUser(UserBL element)
        {
            if (element != null)
            {
                User toUpdate = MapperUser.Map<User>(element);

                if (DB.Users.Read(element.Id) != null)
                {
                    DB.Users.Update(toUpdate);
                    DB.Save();
                }
            }
        }

        /// <inheritdoc/>
        public IEnumerable<TodoListBL> GetTodoLists()
        {
            List<TodoListBL> result = new List<TodoListBL>();

            foreach (var item in DB.TodoLists.ReadAll())
            {
                result.Add(MapperList.Map<TodoListBL>(item));
            }

            return result;
        }

        /// <inheritdoc/>
        public IEnumerable<TodoItemBL> GetTodoItems()
        {
            List<TodoItemBL> result = new List<TodoItemBL>();

            foreach (var item in DB.TodoItems.ReadAll())
            {
                result.Add(MapperItem.Map<TodoItemBL>(item));
            }

            return result;
        }

        /// <inheritdoc/>
        public IEnumerable<UserBL> GetUsers()
        {
            List<UserBL> result = new List<UserBL>();

            foreach (var item in DB.Users.ReadAll())
            {
                result.Add(MapperUser.Map<UserBL>(item));
            }

            return result;
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
