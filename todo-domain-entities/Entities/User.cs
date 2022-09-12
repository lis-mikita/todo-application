using System.ComponentModel.DataAnnotations;

namespace todo_domain_entities.Entities
{
    /// <summary>
    /// The instance of User.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets unique User number in database.
        /// </summary>
        /// <value>
        /// The number of User.
        /// </value>
        [Required]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name of User.
        /// </summary>
        /// <value>
        /// The name of User.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets email of User.
        /// </summary>
        /// <value>
        /// The email of User.
        /// </value>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets password of User.
        /// </summary>
        /// <value>
        /// The password of User.
        /// </value>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the User mode of light/dark on main page UI.
        /// </summary>
        /// <value>
        /// The state light/dark mode.
        /// </value>
        public bool Mode { get; set; }
    }
}
