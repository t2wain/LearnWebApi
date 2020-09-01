using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    public class SalesOrderDetailConfig : IEntityTypeConfiguration<SalesOrderDetail>
    {
        public void Configure(EntityTypeBuilder<SalesOrderDetail> builder)
        {
            var entity = builder;

            entity.HasKey(e => new { e.SalesOrderId, e.SalesOrderDetailId })
                .HasName("PK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID");

            entity.ToTable("SalesOrderDetail", "SalesLT");

            entity.HasComment("Individual products associated with a specific sales order. See SalesOrderHeader.");

            entity.HasIndex(e => e.ProductId);

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_SalesOrderDetail_rowguid")
                .IsUnique();

            entity.Property(e => e.SalesOrderId)
                .HasColumnName("SalesOrderID")
                .HasComment("Primary key. Foreign key to SalesOrderHeader.SalesOrderID.");

            entity.Property(e => e.SalesOrderDetailId)
                .HasColumnName("SalesOrderDetailID")
                .HasComment("Primary key. One incremental unique number per product sold.")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.LineTotal)
                .HasColumnType("numeric(38, 6)")
                .HasComment("Per product subtotal. Computed as UnitPrice * (1 - UnitPriceDiscount) * OrderQty.")
                .HasComputedColumnSql("(isnull(([UnitPrice]*((1.0)-[UnitPriceDiscount]))*[OrderQty],(0.0)))");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.OrderQty).HasComment("Quantity ordered per product.");

            entity.Property(e => e.ProductId)
                .HasColumnName("ProductID")
                .HasComment("Product sold to customer. Foreign key to Product.ProductID.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.UnitPrice)
                .HasColumnType("money")
                .HasComment("Selling price of a single product.");

            entity.Property(e => e.UnitPriceDiscount)
                .HasColumnType("money")
                .HasComment("Discount amount.");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.SalesOrderDetail)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SalesOrder)
                .WithMany(p => p.SalesOrderDetail)
                .HasForeignKey(d => d.SalesOrderId);
        }
    }
}
