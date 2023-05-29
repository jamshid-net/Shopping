using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.DTOs.DtoForProfile
{
    public class UserOrderDto
    {
        public int orderId { get; set; }

        public DateTime? OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

        public bool IsDelivered { get; set; }

    }
}
