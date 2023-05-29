using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.DTO.OrderDto
{
    public class OrderUpdate
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }

        public bool IsCompleted { get; set; }

        public int[] Products { get; set; }

    }
}
