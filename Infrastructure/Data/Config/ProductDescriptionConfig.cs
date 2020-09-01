using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    public class ProductDescriptionConfig : IEntityTypeConfiguration<ProductDescription>
    {
        public void Configure(EntityTypeBuilder<ProductDescription> builder)
        {
            var entity = builder;

            entity.ToTable("ProductDescription", "SalesLT");

            entity.HasComment("Product descriptions in several languages.");

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_ProductDescription_rowguid")
                .IsUnique();

            entity.Property(e => e.ProductDescriptionId)
                .HasColumnName("ProductDescriptionID")
                .HasComment("Primary key for ProductDescription records.");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(400)
                .HasComment("Description of the product.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
        }
    }
}
