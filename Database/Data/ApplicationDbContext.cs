using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Profile> Profiles { get; set; }          
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PanicAlertResolution> PanicAlertResolution { get; set; }
        public DbSet<PanicAlerts> PanicAlerts { get; set; }
        public DbSet<BodyType> BodyType { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
    }
}
