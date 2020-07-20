using AutoMapper;
using Microsoft.DSX.ProjectTemplate.API;
using Microsoft.DSX.ProjectTemplate.Data.Abstractions;
using Microsoft.DSX.ProjectTemplate.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;

namespace Microsoft.DSX.ProjectTemplate.Test.Tests.Integration
{
    /// <summary>
    /// Each integration test is truly isolated because the test has private instances of:
    /// - <see cref="Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory{TEntryPoint}"/>
    /// - <see cref="Microsoft.AspNetCore.TestHost.TestServer"/>
    /// - <see cref="Microsoft.EntityFrameworkCore.InMemory"/> database
    /// </summary>
    [TestCategory("Integration")]
    [TestClass]
    public abstract class BaseIntegrationTest : BaseTest
    {
        /// <summary>
        /// Gets factory that creates <see cref="System.Net.Http.HttpClient"/> instances for sending HTTP requests to.
        /// </summary>
        protected ProjectTemplateWebApplicationFactory<Startup> _factory;

        protected IServiceProvider ServiceProvider { get; }

        protected HttpClient Client { get; }

        protected IMapper Mapper { get; }

        protected BaseIntegrationTest()
        {
            _factory = new ProjectTemplateWebApplicationFactory<Startup>();
            ServiceProvider = _factory.Services;

            Client = _factory.CreateClient();
            Client.DefaultRequestVersion = new Version(2, 0);

            Mapper = _factory.Services.GetRequiredService<IMapper>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Client.Dispose();
            _factory.Dispose();
        }

        /// <summary>
        /// Resolves a <see cref="IEmailService"/> service scope and gets the count of sent emails.
        /// </summary>
        /// <returns>Count of emails sent.</returns>
        protected int GetSentCount()
        {
            using (IServiceScope scope = ServiceProvider.CreateScope())
            {
                var emailService = (FakeEmailService)scope.ServiceProvider.GetRequiredService<IEmailService>();
                return emailService.GetSentCount();
            }
        }
    }
}
