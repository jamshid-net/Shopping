using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.DTO.UserDto.LoginRegister
{
    public class UserRegister
    {
   
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
