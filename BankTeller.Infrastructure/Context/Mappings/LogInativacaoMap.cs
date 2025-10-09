using BankTeller.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankTeller.Infrastructure.Context.Mappings
{
    public class LogInativacaoMap : IEntityTypeConfiguration<LogInativacao>
    {
        public void Configure(EntityTypeBuilder<LogInativacao> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Documento).IsRequired().HasMaxLength(14);
            builder.Property(c => c.DataDesativacao);
            builder.Property(c => c.UsuarioDesativacao).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Descricao);
        }
    }
}
