using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shopping.Domain.Models
{
    [Table("products")]
    public class Product: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      
        [Column("product_id")]
        public int ProductId { get; set; }
        
        [Column("category_id")]
        public int CategoryId { get; set; }
       // [JsonIgnore]
        public Category? Category { get; set; }
        [Column("product_name")]
        public string? ProductName { get; set; }
        [Column("product_description")]
        public string? Description { get; set; } = "no description";

       
        [Column("product_price")]
        public decimal Price { get; set; }
        [Column("product_picture")]
        public string? Picture { get; set; }

        [JsonIgnore]
        public IList<OrderProduct>? OrderProducts { get; set; }

    }
}
