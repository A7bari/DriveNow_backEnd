using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DriveNow.Models;

namespace DriveNow.Data
{
    public class DriveNowContext : DbContext
    {
        public DriveNowContext (DbContextOptions<DriveNowContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().ToTable("Admin");
            modelBuilder.Entity<Owner>().ToTable("Owner");
            modelBuilder.Entity<Tenant>().ToTable("Tenant");
        }

    }
}
