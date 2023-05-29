using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shopping.Domain.Models
{
    public class CartItem
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int CartId { get; set; }
        [JsonIgnore]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow.ToUniversalTime();
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

    }
    
}
