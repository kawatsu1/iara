using Iara.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iara.Infra.Mapping
{
    public class CotacaoMapping : IEntityTypeConfiguration<Cotacao>
    {
        public void Configure(EntityTypeBuilder<Cotacao> builder)
        {
            builder.ToTable("Cotacao");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CNPJComprador)
                .IsRequired(true)
                .HasMaxLength(14)
                .HasColumnType("VARCHAR(14)");

            builder.Property(x => x.CNPJFornecedor)
                .IsRequired(true)
                .HasMaxLength(14)
                .HasColumnType("VARCHAR(14)");

            builder.Property(x => x.NumeroCotacao)
                .IsRequired(true)
                .HasMaxLength(20)
                .HasColumnType("VARCHAR(14)");

            builder.Property(x => x.DataCotacao)
                .IsRequired()
                .HasColumnType("DATETIME2");

            builder.Property(x => x.DataEntregaCotacao)
                .IsRequired()
                .HasColumnType("DATETIME2");

            builder.Property(x => x.CEP)
                .IsRequired(true)
                .HasMaxLength(9)
                .HasColumnType("VARCHAR(9)");

            builder.Property(x => x.Logradouro)
                .HasMaxLength(100)
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.Complemento)
                .HasMaxLength(100)
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.Bairro)
                .HasMaxLength(100)
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.UF)
                .HasMaxLength(2)
                .HasColumnType("VARCHAR(2)");

            builder.Property(x => x.Observacao)
                .HasMaxLength(200)
                .HasColumnType("VARCHAR(200)");
        }
    }
}
