using FluentAssertions;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Test.Tests.Integration.Library
{
    [TestClass]
    [TestCategory("Libraries - Get")]
    public class GetLibraryTests : BaseLibraryTest
    {
        [TestMethod]
        public async Task GetAllLibrary_Valid_Success()
        {
            // Arrange

            // Act
            using var response = await Client.GetAsync("/api/library");

            // Assert
            var result = await EnsureObject<IEnumerable<LibraryDto>>(response);
            result.Should().HaveCountGreaterThan(0);
        }
    }
}