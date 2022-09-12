using System.Collections.Generic;
using todo_aspnetmvc_ui.Models;

namespace todo_aspnetmvc_ui.ViewModels
{
    /// <summary>
    /// Represent data model on Index page.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Gets or sets set of TodoItemModel.
        /// </summary>
        /// <value>
        /// Set of TodoItemModel.
        /// </value>
        public IEnumerable<TodoItemModel> TodoItems { get; set; }

        /// <summary>
        /// Gets or sets set of TodoListModel.
        /// </summary>
        /// <value>
        /// Set of TodoListModel.
        /// </value>
        public IEnumerable<TodoListModel> Notifications { get; set; }

        /// <summary>
        /// Gets or sets set of TodoListModel for notifications.
        /// </summary>
        /// <value>
        /// Set of TodoListModel.
        /// </value>
        public IEnumerable<TodoListModel> TodoLists { get; set; }

        /// <summary>
        /// Gets or sets UserIndexModel.
        /// </summary>
        /// <value>
        /// UserIndexModel object.
        /// </value>
        public UserIndexModel User { get; set; }
    }
}
