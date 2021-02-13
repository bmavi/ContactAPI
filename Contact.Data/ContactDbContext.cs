using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Contact.Models;
using Contact.Data.EntityConfiguration;

namespace Contact.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {

        }

        public DbSet<ContactInfo> contactInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {         

            //UserInfo Entity configuration
            modelBuilder.ApplyConfiguration<ContactInfo>(new ContactConfiguration());
            
        }


    }
}
