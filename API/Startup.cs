using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SQLServer;
using SQLServer.MockRepositories;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
#if DEBUG
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DebugConnection")));
            services.AddScoped<IIssueRepository, IssueRepository>();
#elif RELEASE
#error No Release Database has been configured
            throw new Exception("No Release Database has been configured");
#else
            services.AddSingleton<IIssueRepository, MockIssueRepository>();
#endif

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
