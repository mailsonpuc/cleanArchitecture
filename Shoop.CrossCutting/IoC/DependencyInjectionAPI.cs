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

        // 🟢 1. REPOSITÓRIOS (CAMADA INFRAESTRUTURA) - DESCOMENTADOS! 🟢
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // 🟢 2. SERVICES (CAMADA APPLICATION) - OK 🟢
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();

        // 3. AutoMapper - OK
        services.AddAutoMapper(typeof(DomainToDTOMappingProfile).Assembly);

        return services;
    }
}