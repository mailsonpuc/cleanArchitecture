using System.Reflection;
using Shoop.Application.DTOs;
using Shoop.Domain.Entities;

namespace Shoop.Application.Mappings
{
    public static class DtoMappingExtensions
    {
        public static CategoryDTO ToCategoryDTO(this Category category)
        {
            if (category is null) return null!;

            return new CategoryDTO
            {
                Id = category.Id,
                Title = category.Title
            };
        }

        public static Category ToCategoryEntity(this CategoryInputDTO categoryDto)
        {
            if (categoryDto is null) return null!;

            var category = new Category(categoryDto.Title);
            SetEntityId(category, categoryDto.Id);
            return category;
        }

        public static Category ToCategoryEntity(this CategoryDTO categoryDto)
        {
            if (categoryDto is null) return null!;

            var category = new Category(categoryDto.Title);
            SetEntityId(category, categoryDto.Id);
            return category;
        }

        public static ProductDTO ToProductDTO(this Product product)
        {
            if (product is null) return null!;

            return new ProductDTO
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Category = product.Category
            };
        }

        public static Product ToProductEntity(this ProductInputDTO productDto)
        {
            if (productDto is null) return null!;

            var product = new Product(productDto.Title, productDto.Description, productDto.Price)
            {
                CategoryId = productDto.CategoryId
            };

            SetEntityId(product, productDto.Id);
            return product;
        }

        public static Product ToProductEntity(this ProductDTO productDto)
        {
            if (productDto is null) return null!;

            var product = new Product(productDto.Title, productDto.Description, productDto.Price)
            {
                CategoryId = productDto.CategoryId
            };

            SetEntityId(product, productDto.Id);
            return product;
        }

        public static UserDTO ToUserDTO(this User user)
        {
            if (user is null) return null!;

            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Role = user.Role
            };
        }

        public static User ToUserEntity(this UserDTO userDto)
        {
            if (userDto is null) return null!;

            var user = new User(userDto.Username, userDto.Password, userDto.Role);
            SetEntityId(user, userDto.Id);
            return user;
        }

        private static void SetEntityId<T>(T entity, int id) where T : Entity
        {
            var property = typeof(T).GetProperty(nameof(Entity.Id), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            property?.SetValue(entity, id);
        }
    }
}