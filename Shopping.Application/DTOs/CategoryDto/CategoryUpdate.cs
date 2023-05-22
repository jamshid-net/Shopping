using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.DTOs.CategoryDto
{
    public class CategoryUpdate
    {
        [Required(ErrorMessage ="Enter category id")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Enter category name")]
        public string CategoryName { get; set; }
    }
}
