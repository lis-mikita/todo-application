using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using todo_domain_entities.Entities;
using todo_domain_entities.EntitiesBL;
using todo_domain_entities.Interfaces;

namespace todo_domain_entities
{
    public class BL : IDisposable
    {
        private UnitOfWork DB { get; }
        private Mapper MapperList { get; }
        private Mapper MapperItem { get; }
        private Mapper MapperUser { get; }

        public BL()
        {
            DB = new UnitOfWork();

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

        public void AddTodoList(TodoListBL element)
        {
            DB.TodoLists.Create(MapperList.Map<TodoList>(element));
            DB.Save();
        }

        public void AddTodoItem(TodoItemBL element)
        {
            DB.TodoItems.Create(MapperItem.Map<TodoItem>(element));
            DB.Save();
        }

        public void AddUser(UserBL element)
        {
            DB.Users.Create(MapperUser.Map<User>(element));
            DB.Save();
        }

        public void RemoveTodoList(int id)
        {
            DB.TodoLists.Delete(id);
            DB.Save();
        }

        public void RemoveTodoItem(int id)
        {
            DB.TodoItems.Delete(id);
            DB.Save();
        }

        public void RemoveUser(int id)
        {
            DB.Users.Delete(id);
            DB.Save();
        }

        public TodoListBL FindTodoList(int id)
        {
            return MapperList.Map<TodoListBL>(DB.TodoLists.Read(id));
        }

        public TodoItemBL FindTodoItem(int id)
        {
            return MapperItem.Map<TodoItemBL>(DB.TodoItems.Read(id));
        }

        public UserBL FindUser(int id)
        {
            return MapperUser.Map<UserBL>(DB.Users.Read(id));
        }

        public void UpdateTodoList(TodoListBL element)
        {
             TodoList toUpdate = MapperList.Map<TodoList>(element);
             DB.TodoLists.Update(toUpdate);
             DB.Save();
             //TODO: ADD check
        }

        public void UpdateTodoItem(TodoItemBL element)
        {
            TodoItem toUpdate = MapperItem.Map<TodoItem>(element);
            DB.TodoItems.Update(toUpdate);
            DB.Save();
            //TODO: ADD check
        }

        public void UpdateUser(UserBL element)
        {
            User toUpdate = MapperUser.Map<User>(element);
            DB.Users.Update(toUpdate);
            DB.Save();
            //TODO: ADD check
        }

        public IEnumerable<TodoListBL> GetTodoLists()
        {
            List<TodoListBL> result = new List<TodoListBL>();

            foreach (var item in DB.TodoLists.ReadAll())
            {
                result.Add(MapperList.Map<TodoListBL>(item));
            }

            return result;
        }

        public IEnumerable<TodoItemBL> GetTodoItems()
        {
            List<TodoItemBL> result = new List<TodoItemBL>();

            foreach (var item in DB.TodoItems.ReadAll())
            {
                result.Add(MapperItem.Map<TodoItemBL>(item));
            }

            return result;
        }

        public IEnumerable<UserBL> GetUsers()
        {
            List<UserBL> result = new List<UserBL>();

            foreach (var item in DB.Users.ReadAll())
            {
                result.Add(MapperUser.Map<UserBL>(item));
            }

            return result;
        }

        public void Dispose()
        {
            DB.Dispose();
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
        }

    }
}
