using System.Reflection;
using Application.Contracts.Services;
using Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddOptions();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //Configure App Services
            services.AddScoped<IAuthenticationTokenService, AuthenticationTokenService>();

            return services;
        }
    }
}
