using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using todo_aspnetmvc_ui.Models;
using todo_domain_entities.Interfaces;

namespace todo_aspnetmvc_ui.Controllers
{
    [Route("{controller}")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITodoRepository _todoRepository;

        public HomeController(ILogger<HomeController> logger, ITodoRepository todoRepository)
        {
            _logger = logger;
            _todoRepository = todoRepository;
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
        public string GetAllTodoLists()
        {
            return "Hello";
        }


        //create new To Do lists
        //GET&POST /create


        //add To Do entries(items) to a To Do list
        //enter a title, a description, a due date, a creation date to each To Do item
        //GET&POST /create/list_id=?

        //change To Do items status to: Completed, In Progress, Not Started
        //enter a title, a description, a due date, a creation date to each To Do item
        //GET&PUT /change/list_id=?&item_id=?


        //modify a To Do list
        //GET&PUT /change/list_id=?


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
