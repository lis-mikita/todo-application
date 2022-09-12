namespace todo_domain_entities.EntitiesBL
{
    /// <summary>
    /// The instance of UserBL.
    /// </summary>
    public class UserBL
    {
        /// <summary>
        /// Gets or sets unique UserBL number.
        /// </summary>
        /// <value>
        /// The number of UserBL.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name of UserBL.
        /// </summary>
        /// <value>
        /// The name of UserBL.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets email of UserBL.
        /// </summary>
        /// <value>
        /// The email of UserBL.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets password of UserBL.
        /// </summary>
        /// <value>
        /// The password of UserBL.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the UserBL mode of light/dark on main page UI.
        /// </summary>
        /// <value>
        /// The state light/dark mode.
        /// </value>
        public bool Mode { get; set; }
    }
}
