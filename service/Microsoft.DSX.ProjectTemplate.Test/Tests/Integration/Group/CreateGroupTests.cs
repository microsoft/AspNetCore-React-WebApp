using FluentAssertions;
using Microsoft.DSX.ProjectTemplate.Data;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.DSX.ProjectTemplate.Data.Utilities;
using Microsoft.DSX.ProjectTemplate.Test.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Test.Tests.Integration.Group
{
    [TestClass]
    [TestCategory("Groups - Create")]
    public class CreateGroupTests : BaseGroupTest
    {
        [TestMethod]
        public async Task CreateGroup_ValidDto_Success()
        {
            // Arrange
            var dto = GetGroupDto();

            // Act
            var response = await Client.PostAsJsonAsync("/api/groups", dto);

            // Assert
            var result = await EnsureObject<GroupDto>(response);
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().Be(dto.Name);
            result.IsActive.Should().Be(dto.IsActive);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("\t")]
        public async Task CreateGroup_MissingName_BadRequest(string name)
        {
            // Arrange
            var dto = GetGroupDto();
            dto.Name = name;

            // Act
            var response = await Client.PostAsJsonAsync("/api/groups", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task CreateGroup_NameTooLong_BadRequest()
        {
            // Arrange
            var dto = GetGroupDto();
            dto.Name = RandomFactory.GetAlphanumericString(Constants.MaximumLengths.StringColumn + 1);

            // Act
            var response = await Client.PostAsJsonAsync("/api/groups", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task CreateGroup_NameAlreadyExists_BadRequest()
        {
            // Arrange
            Data.Models.Group randomGroup = null;
            ServiceProvider.ExecuteWithDbScope(db =>
            {
                randomGroup = SeedHelper.GetRandomGroup(db);
            });
            var dto = GetGroupDto();
            dto.Name = randomGroup.Name;

            // Act
            var response = await Client.PostAsJsonAsync("/api/groups", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task CreateGroup_ValidDto_SendsEmail()
        {
            // Arrange
            var dto = GetGroupDto();

            // Act
            var response = await Client.PostAsJsonAsync("/api/groups", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            GetSentCount().Should().Be(1);
        }
    }
}
