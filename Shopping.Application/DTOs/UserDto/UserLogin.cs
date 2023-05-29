using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.DTOs.UserDto
{
    public class UserLogin
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Enter email")]
        public string Email {get; set;} 
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter password")]
        public string Password { get; set; }

    }
}
