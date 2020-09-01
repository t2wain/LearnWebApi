using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            var entity = builder;

            entity.ToTable("Address", "SalesLT");

            entity.HasComment("Street address information for customers.");

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_Address_rowguid")
                .IsUnique();

            entity.HasIndex(e => e.StateProvince);

            entity.HasIndex(e => new { e.AddressLine1, e.AddressLine2, e.City, e.StateProvince, e.PostalCode, e.CountryRegion });

            entity.Property(e => e.AddressId)
                .HasColumnName("AddressID")
                .HasComment("Primary key for Address records.");

            entity.Property(e => e.AddressLine1)
                .IsRequired()
                .HasMaxLength(60)
                .HasComment("First street address line.");

            entity.Property(e => e.AddressLine2)
                .HasMaxLength(60)
                .HasComment("Second street address line.");

            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(30)
                .HasComment("Name of the city.");

            entity.Property(e => e.CountryRegion)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.PostalCode)
                .IsRequired()
                .HasMaxLength(15)
                .HasComment("Postal code for the street address.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.StateProvince)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Name of state or province.");
        }
    }
}
