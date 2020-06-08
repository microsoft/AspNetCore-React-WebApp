using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Microsoft.DSX.ProjectTemplate.Test
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        /// <summary>
        /// Code inside this method runs _before_ <see cref="TStartup"/> does.
        /// </summary>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            
            builder
                .UseEnvironment("Test")
                .UseStartup<TStartup>();

        }
    }
}
