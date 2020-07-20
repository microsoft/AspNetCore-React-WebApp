using FluentAssertions;
using Microsoft.DSX.ProjectTemplate.Data;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.DSX.ProjectTemplate.Data.Utilities;
using Microsoft.DSX.ProjectTemplate.Test.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Test.Tests.Integration.Group
{
    [TestClass]
    [TestCategory("Groups - Update")]
    public class UpdateGroupTests : BaseGroupTest
    {
        [TestMethod]
        public async Task UpdateGroup_ValidDto_Success()
        {
            // Arrange
            var dto = GetGroupDto();
            dto.Id = 4;

            // Act
            var response = await Client.PutAsJsonAsync("/api/groups", dto);

            // Assert
            var result = await EnsureObject<GroupDto>(response);
            result.Id.Should().Be(dto.Id);
            result.Name.Should().Be(dto.Name);
            result.IsActive.Should().Be(dto.IsActive);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public async Task UpdateGroup_InvalidId_BadRequest(int id)
        {
            // Arrange
            Data.Models.Group randomGroup = null;
            ServiceProvider.ExecuteWithDbScope(db =>
            {
                randomGroup = SeedHelper.GetRandomGroup(db);
            });
            var dto = Mapper.Map<GroupDto>(randomGroup);
            dto.Id = id;

            // Act
            var response = await Client.PutAsJsonAsync("/api/groups", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [DataTestMethod]
        [DataRow(int.MaxValue)]
        public async Task UpdateGroup_IdDoesNotExist_NotFound(int id)
        {
            // Arrange
            Data.Models.Group randomGroup = null;
            ServiceProvider.ExecuteWithDbScope(db =>
            {
                randomGroup = SeedHelper.GetRandomGroup(db);
            });
            var dto = Mapper.Map<GroupDto>(randomGroup);
            dto.Id = id;

            // Act
            var response = await Client.PutAsJsonAsync("/api/groups", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("\t")]
        public async Task UpdateGroup_MissingName_BadRequest(string name)
        {
            // Arrange
            Data.Models.Group randomGroup = null;
            ServiceProvider.ExecuteWithDbScope(db =>
            {
                randomGroup = SeedHelper.GetRandomGroup(db);
            });
            var dto = Mapper.Map<GroupDto>(randomGroup);
            dto.Name = name;

            // Act
            var response = await Client.PutAsJsonAsync("/api/groups", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task UpdateGroup_NameTooLong_BadRequest()
        {
            // Arrange
            Data.Models.Group randomGroup = null;
            ServiceProvider.ExecuteWithDbScope(db =>
            {
                randomGroup = SeedHelper.GetRandomGroup(db);
            });
            var dto = Mapper.Map<GroupDto>(randomGroup);
            dto.Name = RandomFactory.GetAlphanumericString(Constants.MaximumLengths.StringColumn + 1);

            // Act
            var response = await Client.PutAsJsonAsync("/api/groups", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task UpdateGroup_NameAlreadyExists_BadRequest()
        {
            // Arrange
            Data.Models.Group randomGroup = null;
            Data.Models.Group differentGroup = null;
            ServiceProvider.ExecuteWithDbScope(async db =>
            {
                randomGroup = SeedHelper.GetRandomGroup(db);
                differentGroup = await db.Groups
                    .Where(x => x.Id != randomGroup.Id)
                    .OrderBy(x => Guid.NewGuid())
                    .FirstAsync();
            });
            var dto = Mapper.Map<GroupDto>(randomGroup);
            dto.Name = differentGroup.Name;

            // Act
            var response = await Client.PutAsJsonAsync("/api/groups", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
