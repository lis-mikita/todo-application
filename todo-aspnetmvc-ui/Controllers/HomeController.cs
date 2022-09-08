using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using todo_aspnetmvc_ui.Models;
using todo_aspnetmvc_ui.ViewModels;
using todo_domain_entities;
using todo_domain_entities.Entities;
using todo_domain_entities.EntitiesBL;

namespace todo_aspnetmvc_ui.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private Mapper MapperList { get; }

        private Mapper MapperItem { get; }

        private Mapper MapperUser { get; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

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

        public void ChangeList(int id, string title, string description)
        {
            using (var db = new BL())
            {
                var todoList = MapperList.Map<TodoListModel>(db.FindTodoList(id));

                if (title != todoList.Title && title != null)
                {
                    todoList.Title = title;
                }
                else if (description != todoList.Description && description != null)
                {
                    todoList.Description = description;
                }

                db.UpdateTodoList(MapperList.Map<TodoListBL>(todoList));
            }
        }

        public void ChangeItem(int id, string title, string description, DateTime datetime, todo_aspnetmvc_ui.Models.TodoItemStatus status)
        {
            using (var db = new BL())
            {
                var todoItem = MapperItem.Map<TodoItemModel>(db.FindTodoItem(id));

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

                db.UpdateTodoItem(MapperItem.Map<TodoItemBL>(todoItem));
            }
        }

        [Route("/hidden/{id}")]
        public IActionResult HiddenList(int id)
        {
            using (var db = new BL())
            {
                var todoList = db.FindTodoList(id);
                
                if (todoList.IsHidden)
                {
                    todoList.IsHidden = false;
                }
                else
                {
                    todoList.IsHidden = true;
                }

                db.UpdateTodoList(MapperList.Map<TodoListBL>(todoList));

                return Redirect($"/{id}");
            }
        }

        [Route("/mode/{id}&{user_id}")]
        public IActionResult ModeChange(int id, int user_id)
        {
            using (var db = new BL())
            {
                var user = db.FindUser(user_id);

                if (user.Mode)
                {
                    user.Mode = false;
                }
                else
                {
                    user.Mode = true;
                }

                db.UpdateUser(MapperList.Map<UserBL>(user));

                return Redirect($"/{id}");
            }
        }

        [Route("/{id?}")]
        public IActionResult Index(int id = 0)
        {
            using (var db = new BL())
            {
                var user = MapperUser.Map<UserModel>(db.GetUsers()
                    .FirstOrDefault(x => x.Email == User.Identity.Name));

                var firstList = db.GetTodoLists().FirstOrDefault(x => x.UserId == user.Id);

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

                var userIndex = new UserIndexModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Mode = user.Mode
                };

                IndexViewModel model = new IndexViewModel()
                {
                    TodoLists = MapperList.Map<List<TodoListModel>>(db.GetTodoLists()
                    .Where(x => x.UserId == user.Id)),
                    User = userIndex
                };

                IEnumerable<TodoItemModel> m = null;
                if (firstList != null)
                {
                    m = MapperItem.Map<List<TodoItemModel>>(db.GetTodoItems())
                        .Where(x => x.ToDoListId == model.TodoLists.FirstOrDefault(x => x.Id == id).Id);
                }

                model.TodoItems = m;

                ViewBag.Id = id;

                return View(model);
            }
        }

        public IActionResult Filter(int id, string sortBy, string groupBy, string filterBy)
        {
            if (string.IsNullOrEmpty(sortBy)
                || string.IsNullOrEmpty(groupBy)
                || string.IsNullOrEmpty(filterBy))
            {
                return Redirect($"/{id}");
            }

            using (var db = new BL())
            {
                var todoItems = MapperItem.Map<List<TodoItemModel>>(db.GetTodoItems())
                    .Where(x => x.ToDoListId == id);

                switch (sortBy)
                {
                    case "title":
                        todoItems = todoItems.OrderBy(x => x.Title);
                        break;
                    case "description":
                        todoItems = todoItems.OrderBy(x => x.Description);
                        break;
                    case "duedate":
                        todoItems = todoItems.OrderBy(x => x.DuetoDateTime);
                        break;
                    case "createdate":
                        todoItems = todoItems.OrderBy(x => x.CreatedDate);
                        break;
                    case "status":
                        todoItems = todoItems.OrderBy(x => x.Status);
                        break;
                    default:
                        todoItems = todoItems.OrderBy(x => x.Id);
                        break;
                }

                switch (groupBy)
                {
                    default:
                        break;
                }

                switch (filterBy)
                {
                    case "completed":
                        todoItems = todoItems.Where(x => x.Status == Models.TodoItemStatus.Completed);
                        break;
                    case "inProgress":
                        todoItems = todoItems.Where(x => x.Status == Models.TodoItemStatus.InProgress);
                        break;
                    case "notStarted":
                        todoItems = todoItems.Where(x => x.Status == Models.TodoItemStatus.NotStarted);
                        break;
                    default:
                        break;
                }

                IndexViewModel ivm = new IndexViewModel()
                {
                    TodoLists = MapperList.Map<List<TodoListModel>>(db.GetTodoLists()),
                    TodoItems = todoItems
                };

                ViewBag.Id = id;
                ViewBag.Sort = sortBy;
                ViewBag.Group = groupBy;
                ViewBag.Filter = filterBy;

                return View(ivm);
            }

        }

        [HttpPost]
        public IActionResult AddList()
        {
            using (var db = new BL())
            {
                var user = MapperUser.Map<UserModel>(db.GetUsers()
                    .FirstOrDefault(x => x.Email == User.Identity.Name));

                int number = db.GetTodoLists()
                    .Where(x => x.UserId == user.Id)
                    .Count(x => x.Title.Contains("Template"));
                number++;

                var todoList = new TodoListModel()
                {
                    Title = $"Template{number}",
                    Description = $"About todolist \"Template{number}\".",
                    IsHidden = false,
                    UserId = user.Id,
                };

                db.AddTodoList(MapperList.Map<TodoListBL>(todoList));
                int id = db.GetTodoLists()
                    .Last(x => x.UserId == user.Id).Id;

                return Redirect($"/{id}");
            }
        }

        [HttpPost]
        public IActionResult AddItem(int id)
        {
            using (var db = new BL())
            {
                int number = 0;
                if (id != 0)
                {
                    number = db.GetTodoItems()
                        .Where(x => x.ToDoListId == id)
                        .Count(x => x.Title.Contains("Template"));
                    number++;
                }
                else
                {
                    var user = MapperUser.Map<UserModel>(db.GetUsers()
                   .FirstOrDefault(x => x.Email == User.Identity.Name));

                    var todoList = new TodoListModel()
                    {
                        Title = $"Template1",
                        Description = $"About todolist \"Template1\".",
                        IsHidden = false,
                        UserId = user.Id,
                    };

                    db.AddTodoList(MapperList.Map<TodoListBL>(todoList));
                    id = db.GetTodoLists()
                        .Last(x => x.UserId == user.Id).Id;
                }

                var todoItem = new TodoItemModel()
                {
                    Title = $"Template{number}",
                    Description = $"About todoitem \"Template{number}\".",
                    DuetoDateTime = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Status = Models.TodoItemStatus.NotStarted,
                    ToDoListId = id
                };

                db.AddTodoItem(MapperItem.Map<TodoItemBL>(todoItem));

                return Redirect($"/{id}");
            }
        }

        [Route("/delete/{id}")]
        public IActionResult DeleteList(int id)
        {
            using (var db = new BL())
            {
                db.RemoveTodoList(id);

                return Redirect($"/0");
            }
        }

        [Route("/delete/item/{list_id}/{id}")]
        public IActionResult DeleteItem(int id, int list_id)
        {
            using (var db = new BL())
            {
                db.RemoveTodoItem(id);

                return Redirect($"/{list_id}");
            }
        }

        [Route("/copy/{id}")]
        public IActionResult CopyList(int id)
        {
            using (var db = new BL())
            {
                var todoListOld = db.FindTodoList(id);

                int number = db.GetTodoLists().Count(x => x.Title.Contains(todoListOld.Title));
                number++;

                var todoListNew = new TodoListModel()
                {
                    Title = $"{todoListOld.Title}{number}",
                    Description = todoListOld.Description,
                    IsHidden = false,
                    UserId = todoListOld.UserId
                };

                db.AddTodoList(MapperList.Map<TodoListBL>(todoListNew));
                int idNew = db.GetTodoLists().Last(x => x.UserId == todoListOld.UserId).Id;

                foreach (var item in MapperItem.Map<List<TodoItemModel>>(db.GetTodoItems().Where(x => x.ToDoListId == id)))
                {
                    var todoItemNew = new TodoItemModel()
                    {
                        Title = item.Title,
                        Description = item.Description,
                        DuetoDateTime = item.DuetoDateTime,
                        Status = item.Status,
                        ToDoListId = idNew,
                        CreatedDate = item.CreatedDate
                    };

                    db.AddTodoItem(MapperItem.Map<TodoItemBL>(todoItemNew));
                }

                return Redirect($"/{idNew}");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
