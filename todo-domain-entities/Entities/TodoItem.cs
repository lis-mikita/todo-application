using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todo_domain_entities.Entities
{
    /// <summary>
    /// Types of TodoItem status.
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
    /// The instance of TodoItem.
    /// </summary>
    public class TodoItem
    {
        private DateTime _createdDate;
        private DateTime _duetoDate;

        /// <summary>
        /// Gets or sets unique TodoItem number in database.
        /// </summary>
        /// <value>
        /// The number of TodoItem.
        /// </value>
        [Required]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets title of TodoItem.
        /// </summary>
        /// <value>
        /// The title of TodoItem.
        /// </value>
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets description of TodoItem.
        /// </summary>
        /// <value>
        /// The description of TodoItem.
        /// </value>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets status of TodoItem.
        /// </summary>
        /// <value>
        /// One of the TodoItemStatus enum values.
        /// </value>
        public TodoItemStatus Status { get; set; }

        /// <summary>
        /// Gets or sets created date of TodoItem.
        /// </summary>
        /// <value>
        /// The value of DateTime in Utc form.
        /// </value>
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate
        {
            get => _createdDate.ToUniversalTime();
            set => _createdDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        /// <summary>
        /// Gets or sets due to date of TodoItem.
        /// </summary>
        /// <value>
        /// The value of DateTime in Utc form.
        /// </value>
        [DataType(DataType.DateTime)]
        [Column("DueTo")]
        public DateTime DuetoDateTime
        {
            get => _duetoDate.ToUniversalTime();
            set => _duetoDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        /// <summary>
        /// Gets or sets the TodoList number to the owned TodoItem.
        /// </summary>
        /// <value>
        /// The number of TodoList to the owned TodoItem.
        /// </value>
        public int? TodoListId { get; set; }

        /// <summary>
        /// Gets or sets the TodoList to the owned TodoItem.
        /// </summary>
        /// <value>
        /// The TodoList to the owned TodoItem.
        /// </value>
        public TodoList TodoList { get; set; }
    }
}
