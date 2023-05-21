using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Domain.Models
{
    [Table("roles_permissions")]
    public class RolePermission
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("role_id")]
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        [Column("permission_id")]
        public int PermissionId { get; set; }
        public Permission? Permission {get;set;}
    }
}