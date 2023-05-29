using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shopping.Domain.Models
{
    [Table("permissions")]
    public class Permission
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("permission_id")]
        public int PermissionId { get; set; }
        [Column("permission_name")]
        public string? PermissionName { get; set; }

        public List<Role>? Roles { get; set; }   
        
        
    }
}