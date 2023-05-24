using AutoMapper;
using Shopping.Application.DTOs.CategoryDto;
using Shopping.Application.DTOs.ProductDto;
using Shopping.Application.DTOs.UserDto;
using Shopping.Domain.Models;

namespace ProductWebApi.AutoMapping
{
    public class AppMapping : Profile
    {

        public AppMapping()
        {
            CreateMap<CategoryAdd, Category>();
            CreateMap<CategoryUpdate, Category>();
            CreateMap<ProductAdd, Product>();
            CreateMap<ProductUpdate, Product>();
            CreateMap<UserRegister, User>();
         
        }

    }
}
