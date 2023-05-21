using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Domain.Models
{
    [Table("user_tokens")]
    public class UserRefreshToken
    {

        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("user_email")]
        [Required]  
        public string UserEmail { get; set; }

        [Column ("user_refreshtoken")]
        [Required]
        public string RefreshToken { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime  ExpiresTime { get; set; }  

    }
}
