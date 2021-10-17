using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NOOS_API.Contracts;
using NOOS_API.Data;
using NOOS_API.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NOOS_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)  // runtime
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddCors(o =>           // allowing universal access for global intrerctions| restrictions will be places on the function level
            {
                o.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
                {
                    Title= "NOOS API", 
                    Version = "v1", 
                    Description= "Saving MONEY!" 
                });

                var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);
                c.IncludeXmlComments(xmlpath);          // saving the xml documentation 
            
            });  // lambda expression for token




            services.AddSingleton<ILoggerService, LoggerService>(); //implementation of the contract
            
            services.AddControllers();   // leave the controller for last (not critical)
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();           //adding swagger
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NOOS API");
                c.RoutePrefix = "";
            });

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
