﻿using Newtonsoft.Json;
using Shopping.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.DTOs.ProductDto
{
    public class ProductAdd
    {

        [Required(ErrorMessage ="Enter category id")]
        public int CategoryId { get; set; }
        
        [Required(ErrorMessage ="Enter name of product")]
        public string? ProductName { get; set; }
        public string? Description { get; set; } = "no description";


        [Required(ErrorMessage ="Enter price of product")]
        public decimal Price { get; set; }

        [Required(ErrorMessage ="Enter url of picture or path")]
        public string? Picture { get; set; }

        // this is used for mapping
        public static implicit operator Product(ProductAdd productadd)
            => new Product
            {
                CategoryId = productadd.CategoryId,
                ProductName = productadd.ProductName,
                Description = productadd.Description,
                Price = productadd.Price,
                Picture = productadd.Picture

            };

    }
}
