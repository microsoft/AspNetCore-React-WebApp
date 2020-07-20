using Microsoft.DSX.ProjectTemplate.Data;
using Microsoft.DSX.ProjectTemplate.Data.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Test.Infrastructure
{
    internal static class Utilities
    {
        /// <summary>
        /// Ensure the database is created and seeded with both real and test data.
        /// </summary>
        /// <typeparam name="TInitializer">The class that initialized the setup, for relevant logging.</typeparam>
        public static void SetupDatabase<TInitializer>(IServiceProvider serviceProvider)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<TInitializer>();

            try
            {
                logger.LogInformation("Database configuration started");

                var db = scope.ServiceProvider.GetRequiredService<ProjectTemplateDbContext>();
                db.Database.EnsureCreated();
                (new TestDataSeeder(db, loggerFactory.CreateLogger<TestDataSeeder>())).SeedTestData();

                logger.LogInformation("Database configuration completed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred setting up the database. Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Asynchronously resolves a <see cref="ProjectTemplateDbContext"/> service scope and executes a delegate function.
        /// The scoped DbContext is disposed after the delegate is executed.
        /// </summary>
        /// <param name="func">The delegate <see cref="Func{T, TResult}"/> to execute with the scoped <see cref="ProjectTemplateDbContext"/></param>
        public static async Task<TResult> ExecuteWithDbScope<TResult>(this IServiceProvider provider, Func<ProjectTemplateDbContext, Task<TResult>> func)
        {
            using (IServiceScope scope = provider.CreateScope())
            {
                ProjectTemplateDbContext db = scope.ServiceProvider.GetRequiredService<ProjectTemplateDbContext>();
                return await func.Invoke(db).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Resolves a <see cref="ProjectTemplateDbContext"/> service scope and executes a delegate action.
        /// The scoped DbContext is disposed after the delegate is executed.
        /// </summary>
        /// <param name="action">The delegate <see cref="Action"/> to execute with the scoped <see cref="ProjectTemplateDbContext"/></param>
        public static void ExecuteWithDbScope(this IServiceProvider provider, Action<ProjectTemplateDbContext> action)
        {
            using (IServiceScope scope = provider.CreateScope())
            {
                ProjectTemplateDbContext db = scope.ServiceProvider.GetRequiredService<ProjectTemplateDbContext>();
                action.Invoke(db);
            }
        }
    }
}
