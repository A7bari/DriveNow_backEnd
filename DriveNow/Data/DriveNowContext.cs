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

        public DbSet<DriveNow.Models.User> User { get; set; } = default!;
    }
}
