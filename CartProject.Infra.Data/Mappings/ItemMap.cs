using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CartProject.Domain.Entities;

namespace CartProject.Infra.Data.Mappings;

public class ItemMap : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder) {
        builder.HasOne(x => x.Cart).WithMany(x => x.Items).HasForeignKey(x => x.CartId);
        builder.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
    }
}
