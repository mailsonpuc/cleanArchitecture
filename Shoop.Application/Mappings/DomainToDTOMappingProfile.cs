using System;
using AutoMapper;
using Shoop.Application.DTOs;
using Shoop.Domain.Entities;

namespace Shoop.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<CategoryInputDTO, CategoryDTO>().ReverseMap();
            CreateMap<CategoryInputDTO, Category>().ReverseMap();
        }
    }
}