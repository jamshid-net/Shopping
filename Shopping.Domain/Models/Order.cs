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
    public class Order
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("order_id")]
        public  int OrderId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        public  User User { get; set; }
        
        [Column("created_at")]
        public DateTime? CreatedDate { get; set; } = DateTime.Now.ToUniversalTime();
        public virtual IList<OrderProduct>? OrderProducts { get; set; }
    }
}
