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

            //  NOVO: Mapeamento do DTO de Entrada para o DTO de Serviço/Saída 
            CreateMap<CategoryInputDTO, CategoryDTO>().ReverseMap();

            // Mapeamento do DTO de Entrada para a Entidade de Domínio (Opcional, mas útil)
            // Se você passar o DTO de entrada diretamente para o Serviço (ver seção 3), você precisará deste mapeamento
            CreateMap<CategoryInputDTO, Category>().ReverseMap();
        }
    }
}