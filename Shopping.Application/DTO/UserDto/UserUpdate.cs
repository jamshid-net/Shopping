using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.DTO.UserDto
{
    public class UserUpdate
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public int[] Roles { get; set; } = new int[1] { 1 };

    }
}
