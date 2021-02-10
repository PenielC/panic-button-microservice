using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class DatabaseModule
    {
        public static void Register(IServiceCollection services, string connection)
        {
            services.AddDbContextPool<ApplicationDbContext>(
              dbContextOptions => dbContextOptions
                  .UseMySql(
                      connection,
                      new MySqlServerVersion(new Version(5, 7, 0)), // use MariaDbServerVersion for MariaDB
                      mySqlOptions => mySqlOptions
                          .CharSetBehavior(CharSetBehavior.NeverAppend))
                  // Everything from this point on is optional but helps with debugging.
                  .EnableSensitiveDataLogging()
                  .EnableDetailedErrors()
            );
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 2;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = false;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}
