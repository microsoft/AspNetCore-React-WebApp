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
    [TestCategory("Library")]
    public class LibraryDtoTests : BaseDtoTest
    {
        [TestMethod]
        public void LibraryDtoValidation_Valid_NoErrors()
        {
            var dto = new LibraryDto
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
        public void LibraryDtoValidation_MissingName_HasValidationErrors(string name)
        {
            var dto = new LibraryDto
            {
                Name = name,
            };

            var validationContext = new ValidationContext(dto);

            var validationResults = dto.Validate(validationContext);

            validationResults.Should().HaveCountGreaterThan(0);
            validationResults.FirstOrDefault(validationResult => validationResult.MemberNames.Any(memberName => memberName.Equals(nameof(LibraryDto.Name))))?.Should().NotBeNull();
        }

        [TestMethod]
        public void LibraryDtoValidation_NameTooLong_HasValidationErrors()
        {
            var dto = new LibraryDto
            {
                Name = RandomFactory.GetAlphanumericString(Constants.MaximumLengths.StringColumn + 1),
            };

            var validationContext = new ValidationContext(dto);

            var validationResults = dto.Validate(validationContext);

            validationResults.Should().HaveCountGreaterThan(0);
            validationResults.FirstOrDefault(validationResult => validationResult.MemberNames.Any(memberName => memberName.Equals(nameof(LibraryDto.Name))))?.Should().NotBeNull();
        }
    }
}
