using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            var entity = builder;

            entity.ToTable("Product", "SalesLT");

            entity.HasComment("Products sold or used in the manfacturing of sold products.");

            entity.HasIndex(e => e.Name)
                .HasName("AK_Product_Name")
                .IsUnique();

            entity.HasIndex(e => e.ProductNumber)
                .HasName("AK_Product_ProductNumber")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_Product_rowguid")
                .IsUnique();

            entity.Property(e => e.ProductId)
                .HasColumnName("ProductID")
                .HasComment("Primary key for Product records.");

            entity.Property(e => e.Color)
                .HasMaxLength(15)
                .HasComment("Product color.");

            entity.Property(e => e.DiscontinuedDate)
                .HasColumnType("datetime")
                .HasComment("Date the product was discontinued.");

            entity.Property(e => e.ListPrice)
                .HasColumnType("money")
                .HasComment("Selling price.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Name of the product.");

            entity.Property(e => e.ProductCategoryId)
                .HasColumnName("ProductCategoryID")
                .HasComment("Product is a member of this product category. Foreign key to ProductCategory.ProductCategoryID. ");

            entity.Property(e => e.ProductModelId)
                .HasColumnName("ProductModelID")
                .HasComment("Product is a member of this product model. Foreign key to ProductModel.ProductModelID.");

            entity.Property(e => e.ProductNumber)
                .IsRequired()
                .HasMaxLength(25)
                .HasComment("Unique product identification number.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SellEndDate)
                .HasColumnType("datetime")
                .HasComment("Date the product was no longer available for sale.");

            entity.Property(e => e.SellStartDate)
                .HasColumnType("datetime")
                .HasComment("Date the product was available for sale.");

            entity.Property(e => e.Size)
                .HasMaxLength(5)
                .HasComment("Product size.");

            entity.Property(e => e.StandardCost)
                .HasColumnType("money")
                .HasComment("Standard cost of the product.");

            entity.Property(e => e.ThumbNailPhoto).HasComment("Small image of the product.");

            entity.Property(e => e.ThumbnailPhotoFileName)
                .HasMaxLength(50)
                .HasComment("Small image file name.");

            entity.Property(e => e.Weight)
                .HasColumnType("decimal(8, 2)")
                .HasComment("Product weight.");

            entity.HasOne(d => d.ProductCategory)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.ProductCategoryId);

            entity.HasOne(d => d.ProductModel)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.ProductModelId);
        }
    }
}
