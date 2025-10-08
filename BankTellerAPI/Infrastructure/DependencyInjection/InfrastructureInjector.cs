using BankTellerAPI.Application.Interfaces;
using BankTellerAPI.Infrastructure.Context;
using BankTellerAPI.Infrastructure.Repositories;

namespace BankTellerAPI.Infrastructure.DependencyInjection
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