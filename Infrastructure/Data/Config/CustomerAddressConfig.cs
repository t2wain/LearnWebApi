using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    public class CustomerAddressConfig : IEntityTypeConfiguration<CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<CustomerAddress> builder)
        {
            var entity = builder;

            entity.HasKey(e => new { e.CustomerId, e.AddressId })
                .HasName("PK_CustomerAddress_CustomerID_AddressID");

            entity.ToTable("CustomerAddress", "SalesLT");

            entity.HasComment("Cross-reference table mapping customers to their address(es).");

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_CustomerAddress_rowguid")
                .IsUnique();

            entity.Property(e => e.CustomerId)
                .HasColumnName("CustomerID")
                .HasComment("Primary key. Foreign key to Customer.CustomerID.");

            entity.Property(e => e.AddressId)
                .HasColumnName("AddressID")
                .HasComment("Primary key. Foreign key to Address.AddressID.");

            entity.Property(e => e.AddressType)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("The kind of Address. One of: Archive, Billing, Home, Main Office, Primary, Shipping");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.HasOne(d => d.Address)
                .WithMany(p => p.CustomerAddress)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.CustomerAddress)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
