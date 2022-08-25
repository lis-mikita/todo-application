using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using todo_aspnetmvc_ui.Models;
using todo_aspnetmvc_ui.Models.EF.Interfaces;
using todo_domain_entities.Entities;

namespace todo_aspnetmvc_ui.Controllers
{
    [Route("{controller}")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITodoRepository _repository;

        public HomeController(ILogger<HomeController> logger, ITodoRepository todoRepository)
        {
            _logger = logger;
            _repository = todoRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //view all To Do lists at the application's list view
        //GET /all
        [HttpGet("/all")]
        public IEnumerable<TodoList> GetAllTodoLists()
        {
            return _repository.TodoLists.ReadAll();
        }


        //create new To Do lists
        //GET&POST /add
        [HttpGet("/add")]
        public IActionResult AddList()
        {
            return View();
        }

        [HttpPost("/add")]
        public IActionResult AddList(string title, string description)
        {
            var todoList = new TodoList()
            {
                Title = title,
                Description = description
            };

            _repository.TodoLists.Create(todoList);
            _repository.Save();

            return View();
        }


        //add To Do entries(items) to a To Do list
        //enter a title, a description, a due date, a creation date to each To Do item
        //GET&POST /add/list_id=?
        [HttpGet("/add/{list_id}")]
        public IActionResult AddItem()
        {
            return View();
        }

        [HttpPost("/add/{list_id}")]
        public IActionResult AddItem(int list_id, string title, string description, DateTime datetime, todo_domain_entities.Entities.TodoItemStatus status)
        {
            var todoItem = new TodoItem()
            {
                Title = title,
                Description = description,
                CreatedDate = DateTime.UtcNow,
                DuetoDateTime = datetime,
                Status = status
            };

            _repository.TodoLists.Read(list_id).TodoItems.Add(todoItem);
            _repository.Save();

            return View();
        }


        //change To Do items
        //GET&PUT /change/list_id=?&item_id=?
        [HttpGet("/change/{list_id}/{item_id}")]
        public IActionResult ChangeItem(int list_id, int item_id)
        {
            var todoItem = _repository.TodoItems.Read(item_id);
            
            ViewBag.TodoItem = todoItem;
            return View();
        }

        [HttpPost("/change/{list_id}/{item_id}")]
        public IActionResult ChangeItem(int list_id, int item_id, string title, string description, DateTime datetime, todo_domain_entities.Entities.TodoItemStatus status)
        {
            var todoItem = new TodoItem()
            {
                Title = title,
                Description = description,
                CreatedDate = _repository.TodoItems.Read(item_id).CreatedDate,
                DuetoDateTime = datetime,
                Status = status,
                Id = item_id
            };

            _repository.TodoItems.Update(todoItem);
            _repository.Save();

            return Redirect($"/change/{list_id}/{item_id}");
        }

        //modify a To Do list
        //GET&PUT /change/list_id=?
        [HttpGet("/change/{list_id}")]
        public IActionResult ChangeList(int list_id)
        {
            var todoList = _repository.TodoLists.Read(list_id);

            ViewBag.TodoList = todoList;
            return View();
        }

        [HttpPost("/change/{list_id}")]
        public IActionResult ChangeList(int list_id, string title, string description)
        {
            var todoList = new TodoList()
            {
                Id = list_id,
                Title = title,
                Description = description,
            };

            _repository.TodoLists.Update(todoList);
            _repository.Save();

            return Redirect($"/change/{list_id}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
