using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Domain.Models
{
    public abstract class BaseEntity
    {
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("created_by")]
        public string? CreatedBy { get; set; }
        [Column("last_modified")]
        public DateTime? LastModified { get; set; }
        [Column("last_modified_by")]
        public string? LastModifiedBy { get; set; }
    }
}
