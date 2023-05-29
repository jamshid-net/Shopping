using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Domain.Models
{
    [Table("orders")]
    public class Order: BaseEntity
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("order_id")]
        public  int OrderId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        public  User? User { get; set; }

        [Column("is_completed")]
        public bool IsCompleted { get; set; } = false;
        [NotMapped]
        public List<Product>? Products { get; set; }
        public IList<OrderProduct>? OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
