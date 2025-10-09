using BankTeller.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankTeller.Infrastructure.Context.Mappings
{
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Documento).IsRequired().HasMaxLength(14);
            builder.Property(c => c.DataAbertura);
            builder.Property(c => c.SaldoAtual);
            builder.Property(c => c.Ativa);
            builder.Property(c => c.DataDesativacao);
            builder.Property(c => c.UsuarioDesativacao);
        }
    }
}
