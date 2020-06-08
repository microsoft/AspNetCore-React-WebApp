using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.DSX.ProjectTemplate.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Microsoft.DSX.ProjectTemplate.API
{
    public class Program
    {
       
        public static void Main(string[] args)
        {

            //var webHost = CreateWebHostBuilder(args).Build();

            IHost host = CreateHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            try
            {
                RunDatabaseMigrations(host, logger);
                host.Run();
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, ex.Message);
                throw;
            }
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>();
                 });


        private static void RunDatabaseMigrations(IHost host, ILogger logger)
        {
            logger.LogInformation($"Running database migrations");
            using (var serviceScope = host.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ProjectTemplateDbContext>();
                context.Database.Migrate();
            }
            logger.LogInformation($"Completed database migrations");
        }
    }
}
