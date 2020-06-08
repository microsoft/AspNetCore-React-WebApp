using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DSX.ProjectTemplate.Command;
using Microsoft.DSX.ProjectTemplate.Data;
using Microsoft.DSX.ProjectTemplate.Data.Abstractions;
using Microsoft.DSX.ProjectTemplate.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Microsoft.DSX.ProjectTemplate.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        readonly string CorsPolicy = "CorsPolicy";

        public IConfiguration Configuration { get; }
        public IHostEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<GlobalExceptionFilter>();

            services
                .AddCors(options =>
                {
                    options.AddPolicy(CorsPolicy,
                        builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
                })
                .AddControllers();

            // Register Entity Framework Core
            ConfigureDatabase(services);

            // Register MediatR and handlers
            services.AddMediatR(typeof(HandlerBase));

            // Register AutoMapper
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            services.AddSingleton(mapperConfig.CreateMapper());

            // Register our custom services.
            services.AddSingleton<IEmailService, EmailService>();

            // Register the Swagger services
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Project Template API";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Devices Software Experiences",
                        Email = "dsx@microsoft.com",
                        Url = "https://deviceswiki.com/wiki/DSX"
                    };
                };
            });
        }

        protected virtual void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<ProjectTemplateDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Database"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();

            if (!env.IsEnvironment("Test"))
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseCors(CorsPolicy);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
