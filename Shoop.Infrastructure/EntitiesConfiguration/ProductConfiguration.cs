using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoop.Domain.Entities;

namespace Shoop.Infrastructure.EntitiesConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(p => p.Title).HasMaxLength(100).IsRequired();

            builder.Property(p => p.Description).HasMaxLength(1000).IsRequired();

            builder.Property(p => p.Price).HasPrecision(18, 2).IsRequired();

            // -----------------------------------------------------------------
            //  CONFIGURAÇÃO DO RELACIONAMENTO (Muitos para Um)
            // -----------------------------------------------------------------

           
            // Informa que a propriedade CategoryId é a FK.
            builder.HasOne(p => p.Category) // O Produto TEM UM Category...
                   .WithMany() // ...e o Category TEM MUITOS Produtos (implícito neste lado)
                   .HasForeignKey(p => p.CategoryId) // ...usando CategoryId como FK.
                   .IsRequired(); // Garante que um Produto DEVE ter uma Categoria.

            // -----------------------------------------------------------------
        }
    }
}