using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    public class SalesOrderHeaderConfig : IEntityTypeConfiguration<SalesOrderHeader>
    {
        public void Configure(EntityTypeBuilder<SalesOrderHeader> builder)
        {
            var entity = builder;

            entity.HasKey(e => e.SalesOrderId)
                .HasName("PK_SalesOrderHeader_SalesOrderID");

            entity.ToTable("SalesOrderHeader", "SalesLT");

            entity.HasComment("General sales order information.");

            entity.HasIndex(e => e.CustomerId);

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_SalesOrderHeader_rowguid")
                .IsUnique();

            entity.HasIndex(e => e.SalesOrderNumber)
                .HasName("AK_SalesOrderHeader_SalesOrderNumber")
                .IsUnique();

            entity.Property(e => e.SalesOrderId)
                .HasColumnName("SalesOrderID")
                .HasComment("Primary key.");

            entity.Property(e => e.AccountNumber)
                .HasMaxLength(15)
                .HasComment("Financial accounting number reference.");

            entity.Property(e => e.BillToAddressId)
                .HasColumnName("BillToAddressID")
                .HasComment("The ID of the location to send invoices.  Foreign key to the Address table.");

            entity.Property(e => e.Comment).HasComment("Sales representative comments.");

            entity.Property(e => e.CreditCardApprovalCode)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasComment("Approval code provided by the credit card company.");

            entity.Property(e => e.CustomerId)
                .HasColumnName("CustomerID")
                .HasComment("Customer identification number. Foreign key to Customer.CustomerID.");

            entity.Property(e => e.DueDate)
                .HasColumnType("datetime")
                .HasComment("Date the order is due to the customer.");

            entity.Property(e => e.Freight)
                .HasColumnType("money")
                .HasDefaultValueSql("((0.00))")
                .HasComment("Shipping cost.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.OnlineOrderFlag)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("0 = Order placed by sales person. 1 = Order placed online by customer.");

            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Dates the sales order was created.");

            entity.Property(e => e.PurchaseOrderNumber)
                .HasMaxLength(25)
                .HasComment("Customer purchase order number reference. ");

            entity.Property(e => e.RevisionNumber).HasComment("Incremental number to track changes to the sales order over time.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SalesOrderNumber)
                .IsRequired()
                .HasMaxLength(25)
                .HasComment("Unique sales order identification number.")
                .HasComputedColumnSql("(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]),N'*** ERROR ***'))");

            entity.Property(e => e.ShipDate)
                .HasColumnType("datetime")
                .HasComment("Date the order was shipped to the customer.");

            entity.Property(e => e.ShipMethod)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Shipping method. Foreign key to ShipMethod.ShipMethodID.");

            entity.Property(e => e.ShipToAddressId)
                .HasColumnName("ShipToAddressID")
                .HasComment("The ID of the location to send goods.  Foreign key to the Address table.");

            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasComment("Order current status. 1 = In process; 2 = Approved; 3 = Backordered; 4 = Rejected; 5 = Shipped; 6 = Cancelled");

            entity.Property(e => e.SubTotal)
                .HasColumnType("money")
                .HasDefaultValueSql("((0.00))")
                .HasComment("Sales subtotal. Computed as SUM(SalesOrderDetail.LineTotal)for the appropriate SalesOrderID.");

            entity.Property(e => e.TaxAmt)
                .HasColumnType("money")
                .HasDefaultValueSql("((0.00))")
                .HasComment("Tax amount.");

            entity.Property(e => e.TotalDue)
                .HasColumnType("money")
                .HasComment("Total due from customer. Computed as Subtotal + TaxAmt + Freight.")
                .HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))");

            entity.HasOne(d => d.BillToAddress)
                .WithMany(p => p.SalesOrderHeaderBillToAddress)
                .HasForeignKey(d => d.BillToAddressId)
                .HasConstraintName("FK_SalesOrderHeader_Address_BillTo_AddressID");

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.SalesOrderHeader)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ShipToAddress)
                .WithMany(p => p.SalesOrderHeaderShipToAddress)
                .HasForeignKey(d => d.ShipToAddressId)
                .HasConstraintName("FK_SalesOrderHeader_Address_ShipTo_AddressID");
        }
    }
}
