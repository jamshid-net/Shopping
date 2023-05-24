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
   [Table("users")]
    public class User: BaseEntity
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }

        [JsonIgnore] 
        public virtual IList<UserRole>? UsersRoles { get; set;}
        [JsonIgnore]
        public IList<Order>? Orders { get; set; }

        [NotMapped]
        [JsonIgnore]
        public int[] Roles { get; set; }  = new int[1] { 1 };
       
    }
}
