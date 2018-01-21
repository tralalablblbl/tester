using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Dto;
using Microsoft.Extensions.Configuration;

namespace Tester.Migrations
{
    public class RoleSeed
    {
        private IConfiguration configuration { get; }
        public RoleSeed(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { Constant.Administrator, Constant.User };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                //creating the roles and seeding them to the database
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new ApplicationRole(roleName));
                }
            }

            // TODO: update all users by role User
            var section = configuration.GetSection(Constant.Settings);
            var user = new ApplicationUser
            {
                UserName = section.GetValue<string>(Constant.DefaultAdminName),
                Email = section.GetValue<string>(Constant.DefaultAdminEmail)
            };

            var _user = await userManager.FindByEmailAsync(user.Email);
            if (_user == null)
            {
                var createAdminUser = await userManager.CreateAsync(user, section.GetValue<string>(Constant.DefaultAdminPassword));
                if (createAdminUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Constant.Administrator);
                }
            }
        }
    }
}
