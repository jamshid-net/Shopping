using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Domain.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Enter name! ")]
        [MaxLength(30)]
        [MinLength(3)]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Enter email! ")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Enter password! ")] 

        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Confirm password!")]
        [Compare("Password", ErrorMessage ="Passwords incorrect!")]
        public string PasswordConfirm { get; set; } 
    }
}
