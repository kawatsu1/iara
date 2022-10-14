using Iara.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iara.Infra.Mapping
{
    public class CotacaoItemMapping : IEntityTypeConfiguration<CotacaoItem>
    {
        public void Configure(EntityTypeBuilder<CotacaoItem> builder)
        {
            builder.ToTable("CotacaoItem");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Cotacao)
                .WithMany(x => x.CotacaoItem)
                .HasForeignKey(x => x.CotacaoId);

            builder.Property(x => x.Descricao)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.NumeroItem)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.Preco)
                .HasColumnType("FLOAT");

            builder.Property(x => x.Quantidade)
                .IsRequired()
                .HasColumnType("INT");

            builder.Property(x => x.Marca)
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.Unidade)
                .HasMaxLength(10)
                .HasColumnType("VARCHAR(10)");
        }
    }
}
