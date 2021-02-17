using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Salon.Abstractions.Interfaces;
using Salon.ADO.DAL;
using Salon.ADO.DAL.Connection;
using Salon.BLL.Interfaces;
using Salon.BLL.Services;
using Salon.Entities.Models;
using Salon.Validation;

namespace Salon.Web
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

            services.AddTransient<ISalonRepository<CustomerEntity>, CustomerRepository>();
            services.AddTransient<ISalonRepository<ServiceEntity>, ServiceRepository>();
            services.AddTransient<ISalonRepository<StateEntity>, StateRepository>();
            services.AddTransient<ISalonRepository<OrderEntity>, OrderRepository>();


            services.AddTransient<ICustomerManager, CustomerManager>();
            services.AddTransient<IServiceManager, ServiceManager>();
            services.AddTransient<IOrderManager, OrderManager>();
            services.AddTransient<IStateManager, StateManager>();
            services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>();
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
