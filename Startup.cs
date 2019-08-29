using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreDocker.Context;
using DotNetCoreDocker.Seeders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotNetCoreDocker
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
            var mysqlHost = Configuration["Database:Host"] ?? "plab-mysql";
            var mysqlPort = Configuration["Database:Port"] ?? "3306";
            var mysqlUser = Configuration["Database:User"] ?? "root";
            var mysqlPass = Configuration["Database:Pass"] ?? "passWord";
            var mysqlDbName = Configuration["Database:DbName"] ?? "OwnTodo";

            Console.WriteLine("mysqlDbName: " + mysqlDbName);

            services.AddDbContext<AppDbContext>(options => options.UseMySql($"server={mysqlHost};userid={mysqlUser};pwd={mysqlPass};port={mysqlPort};database={mysqlDbName}"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            TodoSeeder.Seed(app);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
