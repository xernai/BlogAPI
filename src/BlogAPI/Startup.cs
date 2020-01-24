using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BlogAPI.Utilities;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace BlogAPI
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
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            string connectionString = Configuration.GetConnectionString("BlogPostsContext");

            services.AddDbContextPool<BlogPostsContext>(options => options.UseSqlServer(connectionString));   
    
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
                      //  .AllowCredentials());
            });

            services.AddControllers();
            services.AddScoped(typeof(IDataRepository<>), typeof(DataRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<,>));
            services.AddScoped(typeof(IFileLogger), typeof(FileLogger));

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            // app.UseMvc(routes =>
            // {
            //    routes.MapRoute(
            //        name: "default",
            //      template: "{controller}/{action=Index}/{id?}");
            // });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
               endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
               // To learn more about options for serving an Angular SPA from ASP.NET Core,
               // see https://go.microsoft.com/fwlink/?linkid=864501
               spa.Options.SourcePath = "ClientApp";

               if (env.IsDevelopment())
               {
                   spa.UseAngularCliServer(npmScript: "start");
               }
            });
        }
    }
}
