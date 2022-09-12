using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace todo_domain_entities.Entities
{
    /// <summary>
    /// The instance of TodoList.
    /// </summary>
    public class TodoList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TodoList"/> class.
        /// </summary>
        public TodoList()
        {
            TodoItems = new List<TodoItem>();
        }

        /// <summary>
        /// Gets or sets unique TodoList number in database.
        /// </summary>
        /// <value>
        /// The number of TodoList.
        /// </value>
        [Required]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets title of TodoList.
        /// </summary>
        /// <value>
        /// The title of TodoList.
        /// </value>
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets description of TodoList.
        /// </summary>
        /// <value>
        /// The description of TodoList.
        /// </value>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the TodoList state of hidden/show on main page UI.
        /// </summary>
        /// <value>
        /// The state hidden/show.
        /// </value>
        public bool IsHidden { get; set; }

        /// <summary>
        /// Gets or sets a list of TodoItems in TodoList.
        /// </summary>
        /// <value>
        /// The list of TodoItem.
        /// </value>
        public IEnumerable<TodoItem> TodoItems { get; set; }

        /// <summary>
        /// Gets or sets the User number to the owned TodoList.
        /// </summary>
        /// <value>
        /// The number of User to the owned TodoList.
        /// </value>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the User to the owned TodoList.
        /// </summary>
        /// <value>
        /// The User to the owned TodoList.
        /// </value>
        public User User { get; set; }
    }
}
