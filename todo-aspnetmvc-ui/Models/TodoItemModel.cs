using System;

namespace todo_aspnetmvc_ui.Models
{
    /// <summary>
    /// Types of TodoItemModel status.
    /// </summary>
    public enum TodoItemStatus
    {
        /// <summary>
        /// Represent a completed TodoItem.
        /// </summary>
        Completed,

        /// <summary>
        /// Represent a in progress TodoItem.
        /// </summary>
        InProgress,

        /// <summary>
        /// Represent a not started TodoItem.
        /// </summary>
        NotStarted,
    }

    /// <summary>
    /// The instance of TodoItemModel.
    /// </summary>
    public class TodoItemModel
    {
        private DateTime _createdDate;
        private DateTime _duetoDate;

        /// <summary>
        /// Gets or sets unique TodoItemModel number.
        /// </summary>
        /// <value>
        /// The number of TodoItemModel.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets title of TodoItemModel.
        /// </summary>
        /// <value>
        /// The title of TodoItemModel.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets description of TodoItemModel.
        /// </summary>
        /// <value>
        /// The description of TodoItemModel.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets status of TodoItemModel.
        /// </summary>
        /// <value>
        /// One of the TodoItemStatus enum values.
        /// </value>
        public TodoItemStatus Status { get; set; }

        /// <summary>
        /// Gets or sets created date of TodoItemModel.
        /// </summary>
        /// <value>
        /// The value of DateTime in Utc form.
        /// </value>
        public DateTime CreatedDate
        {
            get => _createdDate.ToUniversalTime();
            set => _createdDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        /// <summary>
        /// Gets or sets due to date of TodoItemModel.
        /// </summary>
        /// <value>
        /// The value of DateTime in Utc form.
        /// </value>
        public DateTime DuetoDateTime
        {
            get => _duetoDate.ToUniversalTime();
            set => _duetoDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        /// <summary>
        /// Gets or sets the TodoListModel number to the owned TodoItemModel.
        /// </summary>
        /// <value>
        /// The number of TodoListModel to the owned TodoItemModel.
        /// </value>
        public int? ToDoListId { get; set; }
    }
}
