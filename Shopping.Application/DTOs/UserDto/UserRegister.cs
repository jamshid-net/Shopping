using System.ComponentModel.DataAnnotations;

namespace Shopping.Application.DTOs.UserDto
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Enter name! ")]
        [MaxLength(30)]
        [MinLength(3)]

        public string UserName { get; set; }
       
        
       
        [EmailAddress(ErrorMessage ="Invalid email type")]
        [Required(ErrorMessage = "Enter email! ")]
        public string Email { get; set; }
       
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter password! ")]

        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm password!")]
        [Compare("Password", ErrorMessage = "Passwords incorrect!")]
        public string PasswordConfirm { get; set; }
    }
}
