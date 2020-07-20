using FluentAssertions;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Test.Tests.Integration.Group
{
    [TestClass]
    [TestCategory("Groups - Get")]
    public class GetGroupTests : BaseGroupTest
    {
        [TestMethod]
        public async Task GetAllGroup_Valid_Success()
        {
            // Arrange

            // Act
            var response = await Client.GetAsync("/api/groups");

            // Assert
            var result = await EnsureObject<IEnumerable<GroupDto>>(response);
            result.Should().HaveCountGreaterThan(0);
        }

        [DataTestMethod]
        [DataRow(1)]
        public async Task GetByIdGroup_ValidId_Success(int groupId)
        {
            // Arrange

            // Act
            var response = await Client.GetAsync($"/api/groups/{groupId}");

            // Assert
            var result = await EnsureObject<GroupDto>(response);
            result.Should().NotBeNull();
            result.Id.Should().Be(groupId);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public async Task GetByIdGroup_InvalidId_BadRequest(int groupId)
        {
            // Arrange

            // Act
            var response = await Client.GetAsync($"/api/groups/{groupId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [DataTestMethod]
        [DataRow(int.MaxValue)]
        public async Task GetByIdGroup_IdDoesNotExist_NotFound(int groupId)
        {
            // Arrange

            // Act
            var response = await Client.GetAsync($"/api/groups/{groupId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
