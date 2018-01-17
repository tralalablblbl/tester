using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=DESKTOP-LCFQBT0\\SQLEXPRESS;Database=Tester;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("DAL"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationRole>().ToTable("Roles");

            builder.Entity<ApplicationUserRole>().ToTable("UserRoles");
            builder.Entity<ApplicationUserClaim>().ToTable("UserClaims");
            builder.Entity<ApplicationUserLogin>().ToTable("UserLogins");

            builder.Entity<ApplicationRoleClaim>().ToTable("RoleClaims");
            builder.Entity<ApplicationUserToken>().ToTable("UserTokens");
        }
    }
}
