using System.ComponentModel.DataAnnotations;

namespace todo_aspnetmvc_ui.ViewModels
{
    /// <summary>
    /// Register form in register page.
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Gets or sets name from user.
        /// </summary>
        /// <value>
        /// Name.
        /// </value>
        [MinLength(1)]
        [MaxLength(50)]
        [Required(ErrorMessage = "Min length for name is 3 symbols")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets email from user.
        /// </summary>
        /// <value>
        /// Email.
        /// </value>
        [Required(ErrorMessage = "Email isn't entered")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets password from user.
        /// </summary>
        /// <value>
        /// Password.
        /// </value>
        [Required(ErrorMessage = "Password isn't entered")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets confirm password from user.
        /// </summary>
        /// <value>
        /// Confirm password.
        /// </value>
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is entered incorrect")]
        public string ConfirmPassword { get; set; }
    }
}
