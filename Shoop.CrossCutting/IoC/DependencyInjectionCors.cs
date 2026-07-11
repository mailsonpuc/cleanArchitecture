using Microsoft.Extensions.DependencyInjection;

namespace Shoop.CrossCutting.IoC
{
    public static class DependencyInjectionCors
    {
        public static IServiceCollection AddInfrastructureCors(this IServiceCollection services)
        {
            const string originsPolicyName = "_originsComAcessoPermitido";

            services.AddCors(options =>
            {
                options.AddPolicy(originsPolicyName, policy =>
                {
                    policy.WithOrigins("https://apirequest.io", "http://localhost:5149")
                        .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            return services;
        }

        public static string GetCorsPolicyName() => "_originsComAcessoPermitido";
    }
}
