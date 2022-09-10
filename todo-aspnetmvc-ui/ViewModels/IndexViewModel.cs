using System.Collections.Generic;
using todo_aspnetmvc_ui.Models;

namespace todo_aspnetmvc_ui.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<TodoItemModel> TodoItems { get; set; }

        public IEnumerable<TodoListModel> Notifications { get; set; }

        public IEnumerable<TodoListModel> TodoLists { get; set; }

        public UserIndexModel User { get; set; }
    }
}
