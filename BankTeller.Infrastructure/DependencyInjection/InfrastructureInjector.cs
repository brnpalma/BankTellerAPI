using BankTeller.Infrastructure.Context;
using BankTeller.Infrastructure.Repositories;
using BankTeller.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BankTeller.Infrastructure.DependencyInjection
{
    public static class InfrastructureInjector
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IContaRepository, ContaRepository>();
            services.AddScoped<BancoContext>();
        }
    }
}