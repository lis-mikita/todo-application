namespace todo_aspnetmvc_ui.Models
{
    /// <summary>
    /// The instance of UserIndexModel.
    /// </summary>
    public class UserIndexModel
    {
        /// <summary>
        /// Gets or sets unique UserIndexModel number in database.
        /// </summary>
        /// <value>
        /// The number of UserIndexModel.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name of UserIndexModel.
        /// </summary>
        /// <value>
        /// The name of UserIndexModel.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets email of UserIndexModel.
        /// </summary>
        /// <value>
        /// The email of UserIndexModel.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the UserIndexModel mode of light/dark on main page UI.
        /// </summary>
        /// <value>
        /// The state light/dark mode.
        /// </value>
        public bool Mode { get; set; }
    }
}
