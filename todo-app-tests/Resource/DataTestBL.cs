using System;
using System.Collections.Generic;
using todo_domain_entities.EntitiesBL;

namespace todo_app_tests.Resource
{
    /// <summary>
    /// The class represent sets of UserBL, TodoListBL and TodoItemBL for mock how database.
    /// </summary>
    public static class DataTestBL
    {
        /// <summary>
        /// Return set of UserBL.
        /// </summary>
        /// <returns>Set of user.</returns>
        public static IEnumerable<UserBL> GetUsersTest()
        {
            List<UserBL> users = new List<UserBL>
            {
                new UserBL
                {
                    Id = 1,
                    Name = "1",
                    Email = "test1@test.com",
                    Password = "TestTest1",
                    Mode = false,
                },
                new UserBL
                {
                    Id = 2,
                    Name = "2",
                    Email = "test2@test.com",
                    Password = "TestTest2",
                    Mode = true,
                },
                new UserBL
                {
                    Id = 3,
                    Name = "3",
                    Email = "test3@test.com",
                    Password = "TestTest3",
                    Mode = false,
                },
            };

            return users;
        }

        /// <summary>
        /// Return set of TodoListBL.
        /// </summary>
        /// <returns>Set of list.</returns>
        public static IEnumerable<TodoListBL> GetTodoListsTest()
        {
            List<TodoListBL> todoLists = new List<TodoListBL>
            {
                new TodoListBL
                {
                    Id = 1,
                    Title = "List1",
                    Description = "List1 of user1",
                    IsHidden = false,
                    UserId = 1,
                },
                new TodoListBL
                {
                    Id = 2,
                    Title = "List2",
                    Description = "List2 of user1",
                    IsHidden = true,
                    UserId = 1,
                },
                new TodoListBL
                {
                    Id = 3,
                    Title = "List3",
                    Description = "List3 of user1",
                    IsHidden = false,
                    UserId = 1,
                },
                new TodoListBL
                {
                    Id = 4,
                    Title = "List4",
                    Description = "List4 of user2",
                    IsHidden = false,
                    UserId = 2,
                },
                new TodoListBL
                {
                    Id = 5,
                    Title = "List5",
                    Description = "List5 of user2",
                    IsHidden = true,
                    UserId = 2,
                },
                new TodoListBL
                {
                    Id = 6,
                    Title = "List6",
                    Description = "List6 of user2",
                    IsHidden = false,
                    UserId = 2,
                },
                new TodoListBL
                {
                    Id = 7,
                    Title = "List7",
                    Description = "List7 of user3",
                    IsHidden = false,
                    UserId = 3,
                },
                new TodoListBL
                {
                    Id = 8,
                    Title = "List8",
                    Description = "List8 of user3",
                    IsHidden = true,
                    UserId = 3,
                },
                new TodoListBL
                {
                    Id = 9,
                    Title = "List9",
                    Description = "List9 of user3",
                    IsHidden = false,
                    UserId = 3,
                },
                new TodoListBL
                {
                    Id = 10,
                    Title = "List10",
                    Description = "List10 of user3",
                    IsHidden = false,
                    UserId = 3,
                },
            };

            return todoLists;
        }

        /// <summary>
        /// Return set of TodoItemBL.
        /// </summary>
        /// <returns>Set of item.</returns>
        public static IEnumerable<TodoItemBL> GetTodoItemsTest()
        {
            List<TodoItemBL> todoItems = new List<TodoItemBL>
            {
                new TodoItemBL
                {
                    Id = 1,
                    Title = "Item1",
                    Description = "Item1 of list1",
                    Status = TodoItemStatus.Completed,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now,
                    ToDoListId = 1,
                },
                new TodoItemBL
                {
                    Id = 2,
                    Title = "Item2",
                    Description = "Item2 of list2",
                    Status = TodoItemStatus.Completed,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now,
                    ToDoListId = 2,
                },
                new TodoItemBL
                {
                    Id = 3,
                    Title = "Item3",
                    Description = "Item3 of list2",
                    Status = TodoItemStatus.InProgress,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now.AddDays(1),
                    ToDoListId = 2,
                },
                new TodoItemBL
                {
                    Id = 4,
                    Title = "Item4",
                    Description = "Item4 of list3",
                    Status = TodoItemStatus.Completed,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now,
                    ToDoListId = 3,
                },
                new TodoItemBL
                {
                    Id = 5,
                    Title = "Item5",
                    Description = "Item5 of list3",
                    Status = TodoItemStatus.InProgress,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now.AddDays(1),
                    ToDoListId = 3,
                },
                new TodoItemBL
                {
                    Id = 6,
                    Title = "Item6",
                    Description = "Item6 of list3",
                    Status = TodoItemStatus.NotStarted,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now.AddDays(1),
                    ToDoListId = 3,
                },
                new TodoItemBL
                {
                    Id = 7,
                    Title = "Item7",
                    Description = "Item7 of list4",
                    Status = TodoItemStatus.Completed,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now,
                    ToDoListId = 4,
                },
                new TodoItemBL
                {
                    Id = 8,
                    Title = "Item8",
                    Description = "Item8 of list5",
                    Status = TodoItemStatus.Completed,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now,
                    ToDoListId = 5,
                },
                new TodoItemBL
                {
                    Id = 9,
                    Title = "Item9",
                    Description = "Item9 of list5",
                    Status = TodoItemStatus.InProgress,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now.AddDays(1),
                    ToDoListId = 5,
                },
                new TodoItemBL
                {
                    Id = 10,
                    Title = "Item10",
                    Description = "Item10 of list6",
                    Status = TodoItemStatus.Completed,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now,
                    ToDoListId = 6,
                },
                new TodoItemBL
                {
                    Id = 11,
                    Title = "Item11",
                    Description = "Item11 of list6",
                    Status = TodoItemStatus.InProgress,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now.AddDays(1),
                    ToDoListId = 6,
                },
                new TodoItemBL
                {
                    Id = 12,
                    Title = "Item12",
                    Description = "Item12 of list6",
                    Status = TodoItemStatus.NotStarted,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now.AddDays(1),
                    ToDoListId = 6,
                },
                new TodoItemBL
                {
                    Id = 13,
                    Title = "Item13",
                    Description = "Item13 of list7",
                    Status = TodoItemStatus.Completed,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now,
                    ToDoListId = 7,
                },
                new TodoItemBL
                {
                    Id = 14,
                    Title = "Item14",
                    Description = "Item14 of list8",
                    Status = TodoItemStatus.Completed,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now,
                    ToDoListId = 8,
                },
                new TodoItemBL
                {
                    Id = 15,
                    Title = "Item15",
                    Description = "Item15 of list8",
                    Status = TodoItemStatus.InProgress,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now.AddDays(1),
                    ToDoListId = 8,
                },
                new TodoItemBL
                {
                    Id = 16,
                    Title = "Item16",
                    Description = "Item16 of list9",
                    Status = TodoItemStatus.Completed,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now,
                    ToDoListId = 9,
                },
                new TodoItemBL
                {
                    Id = 17,
                    Title = "Item17",
                    Description = "Item17 of list9",
                    Status = TodoItemStatus.InProgress,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now.AddDays(1),
                    ToDoListId = 9,
                },
                new TodoItemBL
                {
                    Id = 18,
                    Title = "Item18",
                    Description = "Item18 of list9",
                    Status = TodoItemStatus.NotStarted,
                    CreatedDate = DateTime.Now,
                    DuetoDateTime = DateTime.Now.AddDays(1),
                    ToDoListId = 9,
                },
            };

            return todoItems;
        }
    }
}
