using System.ComponentModel.DataAnnotations;

namespace todo_aspnetmvc_ui.ViewModels
{
    /// <summary>
    /// Login form in login page.
    /// </summary>
    public class LoginModel
    {
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
        /// Gets or sets a value indicating whether gets or sets check box remember user from user.
        /// </summary>
        /// <value>True, if application need remember of user.</value>
        public bool RememberMe { get; set; }
    }
}
