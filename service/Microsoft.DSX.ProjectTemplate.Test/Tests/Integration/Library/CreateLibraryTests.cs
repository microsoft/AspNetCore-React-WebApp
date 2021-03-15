using FluentAssertions;
using Microsoft.DSX.ProjectTemplate.Data;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.DSX.ProjectTemplate.Data.Utilities;
using Microsoft.DSX.ProjectTemplate.Test.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Test.Tests.Integration.Library
{
    [TestClass]
    [TestCategory("Libraries - Create")]
    public class CreateLibraryTests : BaseLibraryTest
    {
        [TestMethod]
        public async Task CreateLibrary_ValidDto_Success()
        {
            // Arrange
            var dto = GetLibraryDto();

            // Act
            using var response = await Client.PostAsJsonAsync("/api/library", dto) ;

            // Assert
            var result = await EnsureObject<LibraryDto>(response);
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().Be(dto.Name);
            
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("\t")]
        public async Task CreateLibrary_MissingName_BadRequest(string name)
        {
            // Arrange
            var dto = GetLibraryDto();
            dto.Name = name;

            // Act
            using var response = await Client.PostAsJsonAsync("/api/library", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task CreateLibrary_NameTooLong_BadRequest()
        {
            // Arrange
            var dto = GetLibraryDto();
            dto.Name = RandomFactory.GetAlphanumericString(Constants.MaximumLengths.StringColumn + 1);

            // Act
            using var response = await Client.PostAsJsonAsync("/api/library", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

       
       
        
       
    }
}
