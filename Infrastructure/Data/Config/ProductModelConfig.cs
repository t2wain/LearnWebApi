using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    public class ProductModelConfig : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            var entity = builder;

            entity.ToTable("ProductModel", "SalesLT");

            entity.HasIndex(e => e.CatalogDescription)
                .HasName("PXML_ProductModel_CatalogDescription");

            entity.HasIndex(e => e.Name)
                .HasName("AK_ProductModel_Name")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_ProductModel_rowguid")
                .IsUnique();

            entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");

            entity.Property(e => e.CatalogDescription).HasColumnType("xml");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())");
        }
    }
}
