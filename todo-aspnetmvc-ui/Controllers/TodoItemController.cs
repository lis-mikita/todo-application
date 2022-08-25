using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using todo_domain_entities;
using todo_domain_entities.EntitiesBL;
using todo_domain_entities.Interfaces;

namespace todo_aspnetmvc_ui.Controllers
{
    public class TodoItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ShowAllItems()
        {
            List<TodoItemBL> foundItems = new List<TodoItemBL>();

            using (var db = new BL())
            {
                foreach (TodoItemBL item in db.GetTodoItems())
                {
                    foundItems.Add(item);
                }
            }

            return View(foundItems);

        }

        //add To Do entries(items) to a To Do list
        //enter a title, a description, a due date, a creation date to each To Do item
        //GET&POST /add/list_id=?
        [HttpGet]
        public IActionResult AddItem(int list_id)
        {
            ViewBag.Title = $"Add your todo item to list \"{list_id}\":";

            return View();
        }

        [HttpPost]
        public IActionResult AddItem(int list_id, string title, string description, DateTime datetime, TodoItemStatus status)
        {
            var todoItem = new TodoItemBL()
            {
                Title = title,
                Description = description,
                CreatedDate = DateTime.UtcNow,
                DuetoDateTime = datetime,
                Status = status,
                ToDoListId = list_id
            };

            using (var db = new BL())
            {
                db.AddTodoItem(todoItem);
            }

            ViewBag.Title = $"Item \"{todoItem.Title}\" was added successful. Can you and list again?";

            return View();

        }

        //change To Do items
        //GET&PUT /change/list_id=?&item_id=?
        [HttpGet]
        public IActionResult ChangeItem(int list_id, int item_id)
        {
            var todoItem = new TodoItemBL();

            using (var db = new BL())
            {
                var bufTodoItem = db.FindTodoItem(item_id);

                if (bufTodoItem != null)
                {
                    todoItem = bufTodoItem;
                }
            }

            if (TempData["Title"] == null)
            {
                TempData["Title"] = $"Change your todo item \"{item_id}\" in list \"{list_id}\":";
            }

            return View(todoItem);

        }

        [HttpPost]
        public IActionResult ChangeItem(int list_id, int item_id, string title, string description, DateTime datetime, todo_domain_entities.EntitiesBL.TodoItemStatus status)
        {
            var todoItem = new TodoItemBL()
            {
                Title = title,
                Description = description,
                DuetoDateTime = datetime,
                Status = status,
                Id = item_id,
                ToDoListId = list_id
            };

            using (var db = new BL())
            {
                todoItem.CreatedDate = db.FindTodoItem(item_id).CreatedDate;

                db.UpdateTodoItem(todoItem);
            }
            TempData["Title"] = $"Item \"{todoItem.Title}\" in list \"{todoItem.ToDoListId}\" was changed successful. Can you change again?";

            return Redirect($"/TodoItem/ChangeItem?list_id={list_id}&item_id={item_id}");

        }
    }
}
