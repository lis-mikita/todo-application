namespace todo_aspnetmvc_ui.Models
{
    /// <summary>
    /// The instance of TodoListModel.
    /// </summary>
    public class TodoListModel
    {
        /// <summary>
        /// Gets or sets unique TodoListModel number.
        /// </summary>
        /// <value>
        /// The number of TodoListModel.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets title of TodoListModel.
        /// </summary>
        /// <value>
        /// The title of TodoListModel.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets description of TodoListModel.
        /// </summary>
        /// <value>
        /// The description of TodoListModel.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the TodoListModel state of hidden/show on main page UI.
        /// </summary>
        /// <value>
        /// The state hidden/show.
        /// </value>
        public bool IsHidden { get; set; }

        /// <summary>
        /// Gets or sets the User number to the owned TodoListModel.
        /// </summary>
        /// <value>
        /// The number of User to the owned TodoListModel.
        /// </value>
        public int? UserId { get; set; }
    }
}
