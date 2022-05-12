using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using BicycleShopDB;
using BicycleShopDB.Data.InitClasses;
using Microsoft.AspNetCore.Identity;
using BicycleShopDB.Tables;

namespace BicycleShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<BicycleContext>();

                    BicycleInit.Initialize(context);
                    ImageInit.Initialize(context);

                    Task adminRegistr = AdminInit.Initialize
                       (
                       services.GetService<IConfiguration>(),
                       services.GetService<UserManager<User>>(),
                       services.GetService<RoleManager<Role>>()
                       );

                    adminRegistr.Wait();
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "DB seeding error");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
