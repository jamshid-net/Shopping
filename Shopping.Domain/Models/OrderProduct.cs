using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shopping.Domain.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }

        public int ProductId { get; set; }
        
        public Product? Product { get; set; }

        
    }
}
