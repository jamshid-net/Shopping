using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Domain.Models
{
    [Table("users_roles")]
    public class UserRole
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]   
        
        public int Id { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; } 
        public User? User { get; set; }

        [Column("role_id")]
        public int? RoleId { get; set; } 
        public Role? Role { get; set; }


    }
}
