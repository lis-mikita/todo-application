using System.ComponentModel.DataAnnotations;

namespace todo_aspnetmvc_ui.ViewModels
{
    public class RegisterModel
    {
        [MinLength(1)]
        [MaxLength(50)]
        [Required(ErrorMessage = "Min length for name is 3 symbols")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email isn't entered")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password isn't entered")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is entered incorrect")]
        public string ConfirmPassword { get; set; }
    }
}
