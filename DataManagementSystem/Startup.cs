using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementSystem.Authentication;
using DataManagementSystem.Configuration;
using DataManagementSystem.Models;
using DataManagementSystem.Repositories;
using DataManagementSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DataManagementSystem
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
            //load basic authentication handler
            services.AddAuthentication("Basic")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);
            services.AddMvc();
            //load config settings
            var configSection = Configuration.GetSection("Config");
            services.Configure<Config>(configSection);
            //load custom services
            //user authentication
            services.AddScoped(typeof(IUserService), typeof(UserService));
            //decryption
            services.AddSingleton(typeof(IDecryptionService), typeof(DecryptionService));
            //repository for the db entity
            services.AddScoped(typeof(IZipFileContentRepository), typeof(ZipFileContentRepository));
            //business layer service
            services.AddScoped(typeof(IZipFileService), typeof(ZipFileService));
            //Setup EF database
            services.AddDbContext<ZipContext>(options => options.UseSqlServer(Configuration["ConnectionString:DmsDb"]));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();

            //migrate database
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<ZipContext>())
                    context.Database.Migrate();
            }
        }
    }
}
