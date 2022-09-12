using System;

namespace todo_domain_entities.EntitiesBL
{
    /// <summary>
    /// Types of TodoItemBL status.
    /// </summary>
    public enum TodoItemStatus
    {
        /// <summary>
        /// Represent a completed TodoItemBL.
        /// </summary>
        Completed,

        /// <summary>
        /// Represent a in progress TodoItemBL.
        /// </summary>
        InProgress,

        /// <summary>
        /// Represent a not started TodoItemBL.
        /// </summary>
        NotStarted,
    }

    /// <summary>
    /// The instance of TodoItemBL.
    /// </summary>
    public class TodoItemBL
    {
        private DateTime _createdDate;
        private DateTime _duetoDate;

        /// <summary>
        /// Gets or sets unique TodoItemBL number.
        /// </summary>
        /// <value>
        /// The number of TodoItemBL.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets title of TodoItemBL.
        /// </summary>
        /// <value>
        /// The title of TodoItemBL.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets description of TodoItemBL.
        /// </summary>
        /// <value>
        /// The description of TodoItemBL.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets status of TodoItemBL.
        /// </summary>
        /// <value>
        /// One of the TodoItemStatus enum values.
        /// </value>
        public TodoItemStatus Status { get; set; }

        /// <summary>
        /// Gets or sets created date of TodoItemBL.
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
        /// Gets or sets due to date of TodoItemBL.
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
        /// Gets or sets the TodoListBL number to the owned TodoItemBL.
        /// </summary>
        /// <value>
        /// The number of TodoListBL to the owned TodoItemBL.
        /// </value>
        public int? ToDoListId { get; set; }
    }
}
