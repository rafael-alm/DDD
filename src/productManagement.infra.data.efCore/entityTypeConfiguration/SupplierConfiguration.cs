using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using productManagement.infra.data.input.entityTypeConfiguration.models;

namespace productManagement.infra.data.input.entityTypeConfiguration
{
    internal class SupplierConfiguration : IEntityTypeConfiguration<SupplierModel>
    {
        public void Configure(EntityTypeBuilder<SupplierModel> builder)
        {
            builder.ToTable("Supplier");

            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id)
                   .HasColumnType("CHAR")
                   .HasMaxLength(36);

            builder.Property(p => p.Code)
                   .UseIdentityColumn()
                   .IsUnicode();

            builder.OwnsOne(
                   o => o.CNPJ,
                   sa => { sa.Property(p => p.Number).HasColumnName("cnpj").HasColumnType("VARCHAR").HasMaxLength(14); });

            builder.Property(x => x.Description)
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(250);
        }
    }
}