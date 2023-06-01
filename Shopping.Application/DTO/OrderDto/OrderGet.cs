using Shopping.Application.DTO.ProductDto;
using Shopping.Application.DTO.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.DTO.OrderDto
{
    public  class OrderGet
    {
        public int OrderId { get; set; }
        public UserGet User { get; set; }
        public List<ProductGet> Products { get; set; }
    }
}
