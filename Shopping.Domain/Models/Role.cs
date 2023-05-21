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
    [Table("roles")]
    public class Role
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("role_name")]
        public string RoleName { get; set; }
        [JsonIgnore]
         public virtual IList<RolePermission>? RolePermissions { get; set; }
        [JsonIgnore]
        public virtual IList<UserRole>? UsersRoles { get; set; }

        public static explicit operator int(Role role) 
            => role.RoleId;
    }

}
