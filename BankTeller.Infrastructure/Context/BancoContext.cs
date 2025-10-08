using BankTellerAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankTeller.Infrastructure.Context
{
    public class BancoContext(DbContextOptions<BancoContext> options) : DbContext(options)
    {
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Transferencia> Transferencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BancoContext).Assembly);
        }
    }
}
