// Shoop.CrossCutting/IoC/DependencyInjectionAPI.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shoop.Domain.Interfaces;
using Shoop.Infrastructure.Context;
using Shoop.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Shoop.Application.Mappings;
using Shoop.Application.Interfaces;
using Shoop.Application.Services;
namespace Shoop.CrossCutting.IoC;

public static class DependencyInjectionAPI
{
    public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("DataBase"));

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();

        // AutoMapper
        services.AddAutoMapper(typeof(DomainToDTOMappingProfile).Assembly);

        return services;
    }
}