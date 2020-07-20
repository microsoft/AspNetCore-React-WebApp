using Microsoft.Extensions.Logging;

namespace Microsoft.DSX.ProjectTemplate.Data.Utilities
{
    public class TestDataSeeder
    {
        private readonly ProjectTemplateDbContext _dbContext;
        private readonly ILogger<TestDataSeeder> _logger;

        public TestDataSeeder(ProjectTemplateDbContext context, ILogger<TestDataSeeder> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public void SeedTestData()
        {
            _logger.LogInformation("Database seeding started.");

            SeedGroups(10);

            SeedProjects(10);

            _logger.LogInformation("Database seeding completed.");
        }

        private void SeedGroups(int entityCount)
        {
            for (int i = 0; i < entityCount; i++)
            {
                var newProject = SeedHelper.CreateValidNewGroup(_dbContext);
                _dbContext.Groups.Add(newProject);
            }

            _dbContext.SaveChanges();
        }

        private void SeedProjects(int entityCount)
        {
            for (int i = 0; i < entityCount; i++)
            {
                var newProject = SeedHelper.CreateValidNewProject(_dbContext);
                _dbContext.Projects.Add(newProject);
            }

            _dbContext.SaveChanges();
        }
    }
}
