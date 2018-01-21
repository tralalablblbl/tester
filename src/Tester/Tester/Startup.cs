using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tester.Models;
using Tester.Services;
using DAL;
using Dto;
using Tester.Migrations;

namespace Tester
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InitializeAndMigrateDb(services);

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ApplicationDbContext>();

            services.AddMvc();
        }
        private void InitializeAndMigrateDb(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString(Constant.DefaultConnection);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            

            // New Ef Db Migration Manager
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            using (var dbContext = new ApplicationDbContext(optionsBuilder.Options))
            {
                Console.WriteLine("Migrating EF db");
                try
                {
                    dbContext.Database.Migrate();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Got exception while migrating DB!");
                    Console.WriteLine(e);
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            var roleSeed = new RoleSeed(Configuration);
            roleSeed.CreateRoles(serviceProvider).Wait();
        }
    }
}
