﻿using System;
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
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<ApplicationUser>().ToTable("Users");
            //builder.Entity<IdentityRole<int>>().ToTable("Roles");

            //builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            //builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            //builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");

            //builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            //builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
        }
    }
}
