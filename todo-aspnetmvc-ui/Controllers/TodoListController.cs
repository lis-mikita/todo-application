using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Xml.Linq;
using todo_domain_entities;
using todo_domain_entities.Entities;
using todo_domain_entities.EntitiesBL;
using todo_domain_entities.Interfaces;

namespace todo_aspnetmvc_ui.Controllers
{
    public class TodoListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //view all To Do lists at the application's list view
        //GET /all
        [HttpGet]
        public IActionResult ShowAllLists()
        {
            List<TodoListBL> foundLists = new List<TodoListBL>();

            using (var db = new BL())
            {
                foreach (TodoListBL item in db.GetTodoLists())
                {
                    foundLists.Add(item);
                }
            }

            return View(foundLists);
        }


        //create new To Do lists
        //GET&POST /add
        [HttpGet]
        public IActionResult AddList()
        {
            ViewBag.Title = "Add your todo item to list:";

            return View();
        }

        [HttpPost]
        public IActionResult AddList(string title, string description)
        {
            var todoList = new TodoListBL()
            {
                Title = title,
                Description = description
            };

            using (var db = new BL())
            {
                db.AddTodoList(todoList);
            }

            ViewBag.Title = $"List \"{todoList.Title}\" was added successful. Can you and list again?";

            return View();
        }

        //modify a To Do list
        //GET&PUT /change/list_id=?
        [HttpGet]
        public IActionResult ChangeList(int list_id)
        {
            var todoList = new TodoListBL();

            using (var db = new BL())
            {
                var bufTodoList = db.FindTodoList(list_id);

                if (bufTodoList != null)
                {
                    todoList = bufTodoList;
                }
            }

            if (TempData["Title"] == null)
            {
                TempData["Title"] = "Change your todo list:";
            }

            return View(todoList);
        }

        [HttpPost]
        public IActionResult ChangeList(int list_id, string title, string description)
        {
            var todoList = new TodoListBL()
            {
                Id = list_id,
                Title = title,
                Description = description,
            };

            using (var db = new BL())
            {
                db.UpdateTodoList(todoList);
            }

            TempData["Title"] = $"List \"{todoList.Title}\" was changed successful. Can you change again?";

            return Redirect($"/TodoList/ChangeList?list_id={list_id}");
        }

    }
}
