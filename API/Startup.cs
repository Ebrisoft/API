using Abstractions.Repositories;
using API.Filters;
using API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SQLServer;
using SQLServer.Models;
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
            //services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));
            services.AddDbContextPool<AppDbContext>(options => options.UseInMemoryDatabase("IssueDb"));

            services.AddScoped<IIssueRepository, IssueRepository>();
            services.AddScoped<ISignInRepository, SignInRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<ILandlordRepository, LandlordRepository>();
            services.AddScoped<IHouseRepository, HouseRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();

            services.AddIdentity<ApplicationUserDbo, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.HttpOnly = true;
                options.SlidingExpiration = true;
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(new ValidationFilter());
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
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

            app.UseOptions();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}