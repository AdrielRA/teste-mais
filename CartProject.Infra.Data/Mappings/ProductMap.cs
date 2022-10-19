using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CartProject.Domain.Entities;

namespace CartProject.Infra.Data.Mappings;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder) {
        // Mapeamento vazio por se tratar de uma implementação in-memory
    }
}
