using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.DTOs.ProductDto
{
    public class ProductUpdate
    {

        [Required(ErrorMessage ="Enter product id for update that product")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Enter category id")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Enter name of product")]
        public string? ProductName { get; set; }

        public string? Description { get; set; } = "no description";


        [Required(ErrorMessage = "Enter price of product")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Enter url of picture or path")]
        public string? Picture { get; set; }
    }
}
