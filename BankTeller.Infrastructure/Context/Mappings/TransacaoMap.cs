using BankTeller.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankTeller.Infrastructure.Context.Mappings
{
    public class TransacaoMap : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.IdContaOrigem).IsRequired();
            builder.Property(c => c.IdContaDestino).IsRequired();
            builder.Property(c => c.Valor).IsRequired();
            builder.Property(c => c.DataTransacao);
        }
    }
}
