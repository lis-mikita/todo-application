using System.ComponentModel.DataAnnotations;

namespace todo_aspnetmvc_ui.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email isn't entered")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password isn't entered")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
