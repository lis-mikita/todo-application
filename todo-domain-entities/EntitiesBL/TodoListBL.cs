namespace todo_domain_entities.EntitiesBL
{
    /// <summary>
    /// The instance of TodoListBL.
    /// </summary>
    public class TodoListBL
    {
        /// <summary>
        /// Gets or sets unique TodoListBL number.
        /// </summary>
        /// <value>
        /// The number of TodoListBL.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets title of TodoListBL.
        /// </summary>
        /// <value>
        /// The title of TodoListBL.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets description of TodoListBL.
        /// </summary>
        /// <value>
        /// The description of TodoListBL.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the TodoListBL state of hidden/show on main page UI.
        /// </summary>
        /// <value>
        /// The state hidden/show.
        /// </value>
        public bool IsHidden { get; set; }

        /// <summary>
        /// Gets or sets the UserBL number to the owned TodoListBL.
        /// </summary>
        /// <value>
        /// The number of UserBL to the owned TodoListBL.
        /// </value>

        public int? UserId { get; set; }
    }
}
