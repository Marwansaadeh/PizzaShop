using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PizzaShop.IRepository;
using PizzaShop.Models;
using PizzaShop.Repositories;

namespace PizzaShop
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);
            var conn = @"Server=DESKTOP-F0J4PGB\SQLEXPRESS;Database=Tomasos;Trusted_Connection=True;ConnectRetryCount=0";

            services.AddDbContext<TomasosContext>(options => options.UseSqlServer(conn));
            services.AddScoped<ICustomer, CustomerRepository>();
            services.AddScoped<IFood, FoodRepository>();
            services.AddScoped<IFoodType, FoodTypeRepository>();
            services.AddScoped<IOrder, OrderRepository>();
            services.AddScoped<IProduct, ProductRepository>();
            services.AddScoped<IOrderFoods, OrderFoodsRepository>();

            services.AddDistributedMemoryCache();
            services.AddSession(option => option.IdleTimeout = TimeSpan.FromMinutes(40));
            services.AddHttpContextAccessor();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "Default",
                template: "{controller=Home}/{action=Index}"
                    );

            });

        }
    
    }
}
