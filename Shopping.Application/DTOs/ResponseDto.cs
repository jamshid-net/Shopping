using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.DTOs
{
    public class ResponseDto<T> 
        where T : class
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = "";
        public bool IsSuccess { get; set; } = true;
        public T Result { get; set; }
    }
}
