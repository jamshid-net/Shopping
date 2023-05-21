using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shopping.Domain.Models
{
    public class CartItem
    {
       
        
        public int Quantity { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]    
        public DateTime DateCreated { get; set; } = DateTime.UtcNow.ToUniversalTime();


        public int ProductId { get; set; }

        [JsonIgnore]    
        public virtual Product? Product { get; set; }

    }
    
}
