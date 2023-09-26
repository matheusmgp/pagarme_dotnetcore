
using Application.Services;
using Application.Services.Authorization;
using Application.Services.Contracts;
using Domain.Contracts.Repositories;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.IoC
{
    public static class AddDependencyExtension
    {
        public static IServiceCollection AddRealStateAPIDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPayableService, PayableService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IPayableRepository, PayableRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtUtils, JwtUtils>();
            return services;
        }
    }
}