using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.DSX.ProjectTemplate.Data;
using Microsoft.DSX.ProjectTemplate.Data.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microsoft.DSX.ProjectTemplate.API
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration, IHostEnvironment hostingEnvironment)
            : base(configuration, hostingEnvironment)
        {
        }

        protected override void ConfigureDatabase(IServiceCollection services)
        {
            services
                .AddDbContext<ProjectTemplateDbContext>(options =>
                    options.UseInMemoryDatabase("InMemoryTestDatabase"), ServiceLifetime.Singleton);

            services
                .AddTransient<TestDataSeeder>();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // perform all configuration in the normal startup
            base.Configure(app, env);

            // seed the database with test data
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var seeder = serviceScope.ServiceProvider.GetService<TestDataSeeder>();
                seeder.SeedTestData();
            }
        }
    }
}
