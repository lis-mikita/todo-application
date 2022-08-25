using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using todo_domain_entities.Entities;
using todo_domain_entities.EntitiesBL;

namespace todo_aspnetmvc_ui.Models
{
    public class TodoListModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<TodoItemModel> TodoItems { get; set; }

        public TodoListModel()
        {
            TodoItems = new List<TodoItemModel>();
        }
    }
}
