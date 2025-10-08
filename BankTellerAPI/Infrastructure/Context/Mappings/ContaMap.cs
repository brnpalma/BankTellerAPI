using BankTellerAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankTellerAPI.Infrastructure.Context.Mappings
{
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.NomeCliente).IsRequired().HasMaxLength(100);
            builder.Property(c => c.DocumentoCliente).IsRequired().HasMaxLength(14);
            builder.Property(c => c.DataAbertura);
            builder.Property(c => c.SaldoAtual);
            builder.Property(c => c.Ativa);
            builder.Property(c => c.DataDesativacao);
            builder.Property(c => c.UsuarioDesativacao);
        }
    }
}
