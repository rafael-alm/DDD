using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using productManagement.domain.aggregates.product;
using productManagement.infra.data.input.entityTypeConfiguration.converters;
using productManagement.infra.data.input.entityTypeConfiguration.models;

namespace productManagement.infra.data.input.entityTypeConfiguration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id)
                   .HasColumnType("CHAR")
                   .HasMaxLength(36);

            builder.Property(p => p.Code)
                   .UseIdentityColumn()
                   .IsUnicode();

            builder.Property(x => x.Description)
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(250);

            builder.Property(p => p.SupplierId)
                   .HasColumnType("CHAR")
                   .HasMaxLength(36);

            builder.Property(p => p.Status)
                   .HasConversion<ConverterStatus>();

            builder.Property(p => p.ManufacturingDate)
                   .HasConversion<ConverterDateOnly>();

            builder.Property(p => p.ExpirationDate)
                   .HasConversion<ConverterDateOnly>();

            builder.HasOne<SupplierModel>() 
                   .WithMany() 
                   .HasForeignKey(o => o.SupplierId); 
        }
    }
}