using FluentAssertions;
using Microsoft.DSX.ProjectTemplate.Data;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.DSX.ProjectTemplate.Data.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Microsoft.DSX.ProjectTemplate.Test.Tests.Unit.DtoValidation
{
    [TestClass]
    [TestCategory("Group")]
    public class GroupDtoTests : BaseDtoTest
    {
        [TestMethod]
        public void GroupDtoValidation_Valid_NoErrors()
        {
            var dto = new GroupDto
            {
                Name = RandomFactory.GetCompanyName(),
            };

            var validationContext = new ValidationContext(dto);

            var validationResults = dto.Validate(validationContext);

            validationResults.Should().HaveCount(0);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void GroupDtoValidation_MissingName_HasValidationErrors(string name)
        {
            var dto = new GroupDto
            {
                Name = name,
            };

            var validationContext = new ValidationContext(dto);

            var validationResults = dto.Validate(validationContext);

            validationResults.Should().HaveCountGreaterThan(0);
            validationResults.FirstOrDefault(validationResult => validationResult.MemberNames.Any(memberName => memberName.Equals(nameof(GroupDto.Name)))).Should().NotBeNull();
        }

        [TestMethod]
        public void GroupDtoValidation_NameTooLong_HasValidationErrors()
        {
            var dto = new GroupDto
            {
                Name = RandomFactory.GetAlphanumericString(Constants.MaximumLengths.StringColumn + 1),
            };

            var validationContext = new ValidationContext(dto);

            var validationResults = dto.Validate(validationContext);

            validationResults.Should().HaveCountGreaterThan(0);
            validationResults.FirstOrDefault(validationResult => validationResult.MemberNames.Any(memberName => memberName.Equals(nameof(GroupDto.Name)))).Should().NotBeNull();
        }
    }
}
