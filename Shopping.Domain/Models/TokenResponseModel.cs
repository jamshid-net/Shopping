using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Domain.Models
{
    public class TokenResponseModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }    
        public string UserEmail { get; set; }   

    }
}
