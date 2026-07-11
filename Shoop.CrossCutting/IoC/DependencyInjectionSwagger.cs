using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Reflection;

namespace Shoop.CrossCutting.IoC
{
    public static class DependencyInjectionSwagger
    {
        public static IServiceCollection AddInfrastructureSwagger(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddOpenApiDocument(options =>
            {
                options.Title = "Shoop API";
                options.Version = "v1";
                options.SchemaSettings.GenerateXmlObjects = true;

                var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
                if (assembly != null)
                {
                    options.Description = $"API do projeto Shoop. Assembly: {assembly.GetName().Name}";
                }

                options.PostProcess = document =>
                {
                    document.Info.Description = "API RESTful para gerenciamento de e-commerce em Clean Architecture";

                    document.Info.Contact = new OpenApiContact
                    {
                        Name = "GitHub Contact",
                        Url = "https://github.com/mailsonpuc"
                    };

                    document.Info.License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = "https://opensource.org/licenses/MIT"
                    };
                };

                options.AddSecurity(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Description = "Informe o token no formato: Bearer {seu_token}"
                    }
                );

                options.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("Bearer")
                );
            });

            return services;
        }
    }
}
