using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Domain.Models
{
    [Table("roles")]
    public class Role
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("role_name")]
        public string RoleName { get; set; }
        
        public List<User>? Users { get; set; } 
        public List<Permission>? Permissions { get; set; }


        public static explicit operator int(Role role) 
            => role.RoleId;
    }

}
