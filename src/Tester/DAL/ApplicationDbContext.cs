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
        public DbSet<Question> Questions { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<Answer> Answers { get; set; }

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

            //https://docs.microsoft.com/ru-ru/ef/core/modeling/relationships ef core does not allow m-to-m
            builder.Entity<UserAnswerAnswer>()
            .HasKey(t => new { t.UserAnswerId, t.AnswerId });

            builder.Entity<UserAnswerAnswer>()
                .HasOne(uaa => uaa.UserAnswer)
                .WithMany(ua => ua.Answers)
                .HasForeignKey(uaa => uaa.UserAnswerId);
        }
    }
}
