using BankTellerAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankTeller.Infrastructure.Context.Mappings
{
    public class TransferenciaMap : IEntityTypeConfiguration<Transferencia>
    {
        public void Configure(EntityTypeBuilder<Transferencia> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.IdContaOrigem).IsRequired();
            builder.Property(c => c.IdContaDestino).IsRequired();
            builder.Property(c => c.Valor).IsRequired();
            builder.Property(c => c.DataTransferencia);
        }
    }
}
