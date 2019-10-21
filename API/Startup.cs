using Abstractions.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SQLServer;
using SQLServer.Repositories;

#pragma warning disable CA1822 // Mark members as static

namespace API
{
    public class Startup
    {
        //  Properties
        //  ==========

        public IConfiguration Configuration { get; }

        //  Constructors
        //  ============

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //  Methods
        //  =======

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            services.AddScoped<IIssueRepository, IssueRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<ILandlordRepository, LandlordRepository>();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<AppDbContext>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}