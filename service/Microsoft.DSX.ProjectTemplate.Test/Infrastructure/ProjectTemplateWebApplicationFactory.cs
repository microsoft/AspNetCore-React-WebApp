using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.DSX.ProjectTemplate.Data;
using Microsoft.DSX.ProjectTemplate.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.DSX.ProjectTemplate.Test.Infrastructure
{
    /// <summary>
    /// Factory for bootstrapping an application into memory for functional end-to-end testing.
    /// </summary>
    public class ProjectTemplateWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly Guid _dbGuid = Guid.NewGuid();

        /// <summary>
        /// Creates an instance of an <see cref="IHostBuilder"/> and adds to its pre-configured defaults
        /// </summary>
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host
                .CreateDefaultBuilder()
                .UseEnvironment(Constants.Environments.Test)
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder
                        .ClearProviders()
                        .AddConsole()
                        .SetMinimumLevel(LogLevel.Trace);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
                        .CaptureStartupErrors(true)
                        .UseStartup<TStartup>();
                });
        }

        /// <summary>
        /// Configures the <see cref="IWebHostBuilder"/> services and sets up the in-memory database
        /// </summary>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureTestServices(services =>
                {
                    services
                        .AddSingleton<IEmailService, FakeEmailService>()
                        .RemoveAll(typeof(IHostedService));
                })
                .ConfigureServices((_, services) =>
                {
                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                    services
                        .AddDbContext<ProjectTemplateDbContext>(options =>
                        {
                            options
                                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                                .UseInMemoryDatabase(_dbGuid.ToString())
                                .EnableSensitiveDataLogging();
                        });

                    var sp = services.BuildServiceProvider();
                    Utilities.SetupDatabase<ProjectTemplateWebApplicationFactory<TStartup>>(sp);
                });
        }
    }
}
