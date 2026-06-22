using Harmonix.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Harmonix.Infrastructure.Data.DbConfig;

public class ProductDbConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("id").HasColumnType("uniqueidentifier");
        builder.Property(p => p.CompanyId).HasColumnName("company_id").HasColumnType("uniqueidentifier");
        builder.Property(p => p.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
        builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").HasColumnType("datetimeoffset").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetimeoffset");
        builder.Property(p => p.Removed).HasColumnName("removed").HasColumnType("bit").HasDefaultValue(false);

        builder.OwnsOne(p => p.Description, d =>
        {
            d.Property(x => x.Value).HasColumnName("description").HasMaxLength(500);
        });

        builder.HasOne(p => p.Company).WithMany().HasForeignKey(p => p.CompanyId);
        builder.HasIndex(p => new { p.CompanyId, p.Code }).IsUnique().HasDatabaseName("idx_products_company_code");
    }
}
