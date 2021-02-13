using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Contact.Models;

namespace Contact.Data.EntityConfiguration
{
    public class ContactConfiguration : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            //Map to table
            builder.ToTable("ContactTbl");

            //Map Primary Key field
            builder.HasKey(c => c.Id);

            //Map Properties
            builder.Property(c => c.Id)
                .IsRequired()
                .HasColumnType<string>("nvarchar(450)");

            builder.Property(c => c.FirstName)
                .IsRequired(false)
                .HasColumnType<string>("nvarchar(200)");

            builder.Property(c => c.LastName)
                .IsRequired(false)
                .HasColumnType<string>("nvarchar(200)");

            builder.Property(c => c.Email)
               .IsRequired(false)
               .HasColumnType<string>("nvarchar(200)");

            builder.Property(c => c.Phone)
               .IsRequired(false)
               .HasColumnType<string>("nvarchar(20)");

            builder.Property(c => c.Status)
                .IsRequired(false)
                .HasColumnType<string>("nvarchar(50)");            

            
        }
    }
}
