using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.DTOs.CategoryDto
{
    public class CategoryAdd
    {
        [Required(ErrorMessage = "Enter category name")]
        public string CategoryName { get; set; }
    }
}
