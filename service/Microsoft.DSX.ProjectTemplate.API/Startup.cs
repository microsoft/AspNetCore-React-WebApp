using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DSX.ProjectTemplate.Command;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Microsoft.DSX.ProjectTemplate.API
{
    /// <summary>
    /// Class that initializes our API.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration of the web API.</param>
        /// <param name="environment">Hosting environment.</param>
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        /// <summary>
        /// This method gets called by the runtime and is used to add services to the DI container.
        /// </summary>
        /// <param name="services">Collection of services to be provided by DI.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbConnections(_configuration, _environment)
                .AddAutoMapperProfiles()
                .AddServices()
                .AddMediatR(typeof(HandlerBase))
                .AddCors()
                .AddSwaggerDocument()
                .AddControllers();
        }

        /// <summary>
        /// This method gets called by the runtime and is used to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        public virtual void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app
                .UseExceptionHandling()
                .UseOpenApi()
                .UseSwaggerUi3()
                .UseRouting()
                .UseCors("CorsPolicy")
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
