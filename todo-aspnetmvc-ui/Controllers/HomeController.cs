using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using todo_aspnetmvc_ui.Contexts;
using todo_aspnetmvc_ui.Models;
using todo_aspnetmvc_ui.ViewModels;
using todo_domain_entities.EntitiesBL;
using todo_domain_entities.Interfaces;

namespace todo_aspnetmvc_ui.Controllers
{
    /// <summary>
    /// Provides display of list page.
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IBL _db;

        private Mapper MapperList { get; }

        private Mapper MapperItem { get; }

        private Mapper MapperUser { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="bl">Session with database.</param>
        public HomeController(IBL bl)
        {
            _db = bl;

            var configList = new MapperConfiguration(cfg =>
                cfg.CreateMap<TodoListBL, TodoListModel>().ReverseMap());
            MapperList = new Mapper(configList);

            var configItem = new MapperConfiguration(cfg =>
                cfg.CreateMap<TodoItemBL, TodoItemModel>().ReverseMap());
            MapperItem = new Mapper(configItem);

            var configUser = new MapperConfiguration(cfg =>
                cfg.CreateMap<UserBL, UserModel>().ReverseMap());
            MapperUser = new Mapper(configUser);
        }

        /// <summary>
        /// Display list page.
        /// </summary>
        /// <param name="id">Number of TodoList.</param>
        /// <returns>Index view page.</returns>
        [Route("/{id?}")]
        public IActionResult Index(int id = 0)
        {
            var user = MapperUser.Map<UserModel>(_db.GetUsers()
                .FirstOrDefault(x => x.Email == User.Identity.Name));

            // Find - Does have user at least list?
            var firstList = _db.GetTodoLists().FirstOrDefault(x => x.UserId == user.Id);

            if (firstList == null)
            {
                // Not action
            }
            else
            {
                if (id == 0)
                {
                    id = firstList.Id;
                }
            }

            var userIndex = new UserIndexModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Mode = user.Mode,
            };

            var todoLists = MapperList.Map<List<TodoListModel>>(_db.GetTodoLists()
                .Where(x => x.UserId == user.Id));

            // Take items list
            var items = MapperItem.Map<List<TodoItemModel>>(_db.GetTodoItems());
            IEnumerable<TodoItemModel> listItems = null;
            if (firstList != null)
            {
                listItems = items.Where(x => x.ToDoListId == todoLists.FirstOrDefault(x => x.Id == id).Id);
            }

            // Take notifications
            var notificationList = new List<TodoListModel>();
            foreach (var item in todoLists ?? Enumerable.Empty<TodoListModel>())
            {
                if (items.FirstOrDefault(x => x.ToDoListId == item.Id && x.DuetoDateTime.Date == DateTime.Today) != null)
                {
                    notificationList.Add(item);
                }
            }

            IndexViewModel model = new IndexViewModel
            {
                TodoLists = todoLists,
                User = userIndex,
                TodoItems = listItems,
                Notifications = notificationList,
            };

            ViewBag.Id = id;

            if (new TypeBrowser(HttpContext).IsMobileDeviceBrowser())
            {
                return View("Index.Mobile", model);
            }

            return View(model);
        }

        /// <summary>
        /// Display list page with filter item.
        /// </summary>
        /// <param name="id">List number.</param>
        /// <param name="sortBy">Sorting.</param>
        /// <param name="groupBy">Grouping.</param>
        /// <param name="filterBy">Filtering.</param>
        /// <returns>Index view page with filters.</returns>
        public IActionResult Filter(int id, string sortBy, string groupBy, string filterBy)
        {
            if (string.IsNullOrEmpty(sortBy)
                || string.IsNullOrEmpty(groupBy)
                || string.IsNullOrEmpty(filterBy))
            {
                return Redirect($"/{id}");
            }

            // Take items for filter
            var todoItems = MapperItem.Map<List<TodoItemModel>>(_db.GetTodoItems())
                .Where(x => x.ToDoListId == id);

            todoItems = sortBy switch
            {
                "title" => todoItems.OrderBy(x => x.Title),
                "description" => todoItems.OrderBy(x => x.Description),
                "duedate" => todoItems.OrderBy(x => x.DuetoDateTime),
                "createdate" => todoItems.OrderBy(x => x.CreatedDate),
                "status" => todoItems.OrderBy(x => x.Status),
                _ => todoItems.OrderBy(x => x.Id),
            };

            // Add switch for groupBy

            todoItems = filterBy switch
            {
                "completed" => todoItems.Where(x => x.Status == Models.TodoItemStatus.Completed),
                "inProgress" => todoItems.Where(x => x.Status == Models.TodoItemStatus.InProgress),
                "notStarted" => todoItems.Where(x => x.Status == Models.TodoItemStatus.NotStarted),
                _ => todoItems.Select(x => x),
            };

            // Take user
            var user = MapperUser.Map<UserModel>(_db.GetUsers()
                .FirstOrDefault(x => x.Email == User.Identity.Name));

            var userIndex = new UserIndexModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Mode = user.Mode,
            };

            // Take lists
            var todoLists = MapperList.Map<List<TodoListModel>>(_db.GetTodoLists()
                    .Where(x => x.UserId == user.Id));

            var items = MapperItem.Map<List<TodoItemModel>>(_db.GetTodoItems());
            var notificationList = new List<TodoListModel>();
            foreach (var item in todoLists ?? Enumerable.Empty<TodoListModel>())
            {
                if (items.FirstOrDefault(x => x.ToDoListId == item.Id && x.DuetoDateTime.Date == DateTime.Today) != null)
                {
                    notificationList.Add(item);
                }
            }

            IndexViewModel ivm = new IndexViewModel
            {
                TodoLists = todoLists,
                TodoItems = todoItems,
                User = userIndex,
                Notifications = notificationList,
            };

            ViewBag.Id = id;
            ViewBag.Sort = sortBy;
            ViewBag.Group = groupBy;
            ViewBag.Filter = filterBy;

            if (new TypeBrowser(HttpContext).IsMobileDeviceBrowser())
            {
                return View("Index.Mobile", ivm);
            }

            return View("Index", ivm);
        }

        /// <summary>
        /// Update TodoList.
        /// </summary>
        /// <param name="id">List number.</param>
        /// <param name="title">New list title.</param>
        /// <param name="description">New list description.</param>
        public void ChangeList(int id, string title, string description)
        {
            var todoList = MapperList.Map<TodoListModel>(_db.FindTodoList(id));

            if (title != todoList.Title && title != null)
            {
                todoList.Title = title;
            }
            else if (description != todoList.Description && description != null)
            {
                todoList.Description = description;
            }
            else
            {
                // Not use
            }

            _db.UpdateTodoList(MapperList.Map<TodoListBL>(todoList));
        }

        /// <summary>
        /// Update TodoItem.
        /// </summary>
        /// <param name="id">Item number.</param>
        /// <param name="title">New item title.</param>
        /// <param name="description">New item description.</param>
        /// <param name="datetime">New item due to date.</param>
        /// <param name="status">New item status.</param>
        public void ChangeItem(int id, string title, string description, DateTime datetime, todo_aspnetmvc_ui.Models.TodoItemStatus status)
        {
            var todoItem = MapperItem.Map<TodoItemModel>(_db.FindTodoItem(id));

            if (title != todoItem.Title && title != null)
            {
                todoItem.Title = title;
            }
            else if (description != todoItem.Description && description != null)
            {
                todoItem.Description = description;
            }
            else if (datetime != todoItem.DuetoDateTime)
            {
                todoItem.DuetoDateTime = datetime;
            }
            else if (status != todoItem.Status)
            {
                todoItem.Status = status;
            }
            else
            {
                // Not use
            }

            _db.UpdateTodoItem(MapperItem.Map<TodoItemBL>(todoItem));
        }

        /// <summary>
        /// Update hidden status of TodoList.
        /// </summary>
        /// <param name="id">List number.</param>
        public void HiddenList(int id)
        {
            var todoList = _db.FindTodoList(id);

            todoList.IsHidden = !todoList.IsHidden;

            _db.UpdateTodoList(MapperList.Map<TodoListBL>(todoList));
        }

        /// <summary>
        /// Change start mode user light/dark.
        /// </summary>
        public void ModeChange()
        {
            int user_id = _db.GetUsers()
                .FirstOrDefault(x => x.Email == User.Identity.Name).Id;
            var user = _db.FindUser(user_id);

            user.Mode = !user.Mode;

            _db.UpdateUser(MapperList.Map<UserBL>(user));
        }

        /// <summary>
        /// Add TodoList in set.
        /// </summary>
        /// <returns>Index page with added list.</returns>
        [HttpPost]
        public IActionResult AddList()
        {
            var user = MapperUser.Map<UserModel>(_db.GetUsers()
                .FirstOrDefault(x => x.Email == User.Identity.Name));

            int number = _db.GetTodoLists()
                .Where(x => x.UserId == user.Id)
                .Count(x => x.Title.Contains("Template", StringComparison.OrdinalIgnoreCase));
            number++;

            var todoList = new TodoListModel
            {
                Title = $"Template{number}",
                Description = $"About todolist \"Template{number}\".",
                IsHidden = false,
                UserId = user.Id,
            };

            _db.AddTodoList(MapperList.Map<TodoListBL>(todoList));

            int id = _db.GetTodoLists()
                .Last(x => x.UserId == user.Id).Id;

            return Redirect($"/{id}");
        }

        /// <summary>
        /// Add TodoItem in set.
        /// </summary>
        /// <param name="id">Item number.</param>
        /// <returns>Index page with added item.</returns>
        [HttpPost]
        public IActionResult AddItem(int id)
        {
            int number = 0;

            // Check - Does have user at least list?
            if (id != 0)
            {
                number = _db.GetTodoItems()
                    .Where(x => x.ToDoListId == id)
                    .Count(x => x.Title.Contains("Template", StringComparison.OrdinalIgnoreCase));
                number++;
            }
            else
            {
                var user = MapperUser.Map<UserModel>(_db.GetUsers()
                    .FirstOrDefault(x => x.Email == User.Identity.Name));

                var todoList = new TodoListModel
                {
                    Title = $"Template1",
                    Description = $"About todolist \"Template1\".",
                    IsHidden = false,
                    UserId = user.Id,
                };

                _db.AddTodoList(MapperList.Map<TodoListBL>(todoList));

                id = _db.GetTodoLists()
                    .Last(x => x.UserId == user.Id).Id;
            }

            var todoItem = new TodoItemModel
            {
                Title = $"Template{number}",
                Description = $"About todoitem \"Template{number}\".",
                DuetoDateTime = DateTime.Now,
                CreatedDate = DateTime.Now,
                Status = Models.TodoItemStatus.NotStarted,
                ToDoListId = id,
            };

            _db.AddTodoItem(MapperItem.Map<TodoItemBL>(todoItem));

            return Redirect($"/{id}");
        }

        /// <summary>
        /// Delete TodoList from set.
        /// </summary>
        /// <param name="id">List number.</param>
        /// <returns>Index page with default list.</returns>
        [Route("/delete/{id}")]
        public IActionResult DeleteList(int id)
        {
            _db.RemoveTodoList(id);

            const int ID_DEFAULT_LIST = 0;

            return Redirect($"/{ID_DEFAULT_LIST}");
        }

        /// <summary>
        /// Delete TodoItem from set.
        /// </summary>
        /// <param name="id">Item number.</param>
        /// <param name="list_id">List number.</param>
        /// <returns>List without item.</returns>
        [Route("/delete/item/{list_id}/{id}")]
        public IActionResult DeleteItem(int id, int list_id)
        {
            _db.RemoveTodoItem(id);

            return Redirect($"/{list_id}");
        }

        /// <summary>
        /// Copy TodoList and create it copy in set.
        /// </summary>
        /// <param name="id">List number.</param>
        /// <returns>Index page with new list.</returns>
        [Route("/copy/{id}")]
        public IActionResult CopyList(int id)
        {
            var todoListOld = _db.FindTodoList(id);

            int number = _db.GetTodoLists().Count(x => x.Title.Contains(todoListOld.Title, StringComparison.OrdinalIgnoreCase));
            number++;

            var todoListNew = new TodoListModel
            {
                Title = $"{todoListOld.Title}{number}",
                Description = todoListOld.Description,
                IsHidden = false,
                UserId = todoListOld.UserId,
            };

            _db.AddTodoList(MapperList.Map<TodoListBL>(todoListNew));

            int newId = _db.GetTodoLists().Last(x => x.UserId == todoListOld.UserId).Id;

            // Don't work mapper
            foreach (var item in MapperItem.Map<List<TodoItemModel>>(_db.GetTodoItems().Where(x => x.ToDoListId == id)))
            {
                var todoItemNew = new TodoItemModel
                {
                    Title = item.Title,
                    Description = item.Description,
                    DuetoDateTime = item.DuetoDateTime,
                    Status = item.Status,
                    ToDoListId = newId,
                    CreatedDate = item.CreatedDate,
                };

                _db.AddTodoItem(MapperItem.Map<TodoItemBL>(todoItemNew));
            }

            return Redirect($"/{newId}");
        }

        /// <summary>
        /// Called when an error occurs.
        /// </summary>
        /// <returns>Error page.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
