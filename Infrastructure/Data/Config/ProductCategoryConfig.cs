using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    public class ProductCategoryConfig : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            var entity = builder;

            entity.ToTable("ProductCategory", "SalesLT");

            entity.HasComment("High-level product categorization.");

            entity.HasIndex(e => e.Name)
                .HasName("AK_ProductCategory_Name")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_ProductCategory_rowguid")
                .IsUnique();

            entity.Property(e => e.ProductCategoryId)
                .HasColumnName("ProductCategoryID")
                .HasComment("Primary key for ProductCategory records.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Category description.");

            entity.Property(e => e.ParentProductCategoryId)
                .HasColumnName("ParentProductCategoryID")
                .HasComment("Product category identification number of immediate ancestor category. Foreign key to ProductCategory.ProductCategoryID.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.HasOne(d => d.ParentProductCategory)
                .WithMany(p => p.InverseParentProductCategory)
                .HasForeignKey(d => d.ParentProductCategoryId)
                .HasConstraintName("FK_ProductCategory_ProductCategory_ParentProductCategoryID_ProductCategoryID");
        }
    }
}
