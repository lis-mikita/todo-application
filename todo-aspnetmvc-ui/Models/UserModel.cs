namespace todo_aspnetmvc_ui.Models
{
    /// <summary>
    /// The instance of UserModel.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Gets or sets unique UserModel number.
        /// </summary>
        /// <value>
        /// The number of UserModel.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name of UserModel.
        /// </summary>
        /// <value>
        /// The name of UserModel.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets email of UserModel.
        /// </summary>
        /// <value>
        /// The email of UserModel.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets password of UserModel.
        /// </summary>
        /// <value>
        /// The password of UserModel.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the UserModel mode of light/dark on main page UI.
        /// </summary>
        /// <value>
        /// The state light/dark mode.
        /// </value>
        public bool Mode { get; set; }
    }
}
