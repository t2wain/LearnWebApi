using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            var entity = builder;

            entity.ToTable("Customer", "SalesLT");

            entity.HasComment("Customer information.");

            entity.HasIndex(e => e.EmailAddress);

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_Customer_rowguid")
                .IsUnique();

            entity.Property(e => e.CustomerId)
                .HasColumnName("CustomerID")
                .HasComment("Primary key for Customer records.");

            entity.Property(e => e.CompanyName)
                .HasMaxLength(128)
                .HasComment("The customer's organization.");

            entity.Property(e => e.EmailAddress)
                .HasMaxLength(50)
                .HasComment("E-mail address for the person.");

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("First name of the person.");

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Last name of the person.");

            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .HasComment("Middle name or middle initial of the person.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.NameStyle).HasComment("0 = The data in FirstName and LastName are stored in western style (first name, last name) order.  1 = Eastern style (last name, first name) order.");

            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasComment("Password for the e-mail account.");

            entity.Property(e => e.PasswordSalt)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComment("Random value concatenated with the password string before the password is hashed.");

            entity.Property(e => e.Phone)
                .HasMaxLength(25)
                .HasComment("Phone number associated with the person.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SalesPerson)
                .HasMaxLength(256)
                .HasComment("The customer's sales person, an employee of AdventureWorks Cycles.");

            entity.Property(e => e.Suffix)
                .HasMaxLength(10)
                .HasComment("Surname suffix. For example, Sr. or Jr.");

            entity.Property(e => e.Title)
                .HasMaxLength(8)
                .HasComment("A courtesy title. For example, Mr. or Ms.");
        }
    }
}
