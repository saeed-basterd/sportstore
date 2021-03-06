﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Data;
using SportStore.Models;
using SportStore.Repository;

namespace SportStore
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
            services
                .AddTransient<IProductRepository, EfProductRepository>();
            services
                .AddDbContext<SportStoreDataContext>(options =>
                    options.UseSqlServer(
                        Configuration["Data:SportStoreProducts:ConnectionString"]));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseStatusCodePages();

            app.UseStaticFiles();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new {controller = "Product", action = "List"}
                );

                routes.MapRoute(
                    name: null,
                    template: "Page{productPage:int}",
                    defaults: new {controller = "Product", action = "List", productPage = 1}
                );

                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new {controller = "Product", action = "List", productPage = 1});

                routes.MapRoute(name: null,
                    template: "",
                    defaults: new {controller = "Product", action = "List", productPage = 1});

                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            });

            app.EnsurePopulated();
        }
    }
}