using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Domain.Models
{
    [Table("order_products")]
    public class OrderProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]   
        
        public int Id { get; set; }
        [Column("order_id")]
        public int OrderId { get; set; }    
        public Order Order { get; set; }


        [Column("product_id")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

    }
}
