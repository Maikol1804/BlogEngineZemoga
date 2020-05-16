using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogEngine.DataAccess;
using BlogEngine.DataAccess.Context;
using BlogEngine.DataAccess.Implementations;
using BlogEngine.DataAccess.Initializer;
using BlogEngine.DataAccess.Interfaces;
using BlogEngine.DataAccess.Models;
using BlogEngine.Services.Contracts;
using BlogEngine.Services.Implementations;
using BlogEngine.Services.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BlogEngine
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<BlogEngineContext>(opt => opt.UseInMemoryDatabase("BlogEngine"));
            services.AddControllers().AddXmlDataContractSerializerFormatters();
            services.AddScoped<IDbInitializer, DbInitializer>();

            //var builder = new ContainerBuilder();

            //builder.RegisterModule(new Configuration());

            //builder.Populate(services);

            //IContainer container = builder.Build();
            //return container.Resolve<IServiceProvider>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new IoCConfiguration());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
                dbInitializer.SeedData();
            }

        }

    }
}
