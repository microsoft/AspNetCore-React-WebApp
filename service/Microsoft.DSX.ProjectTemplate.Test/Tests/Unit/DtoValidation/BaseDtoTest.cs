using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Microsoft.DSX.ProjectTemplate.Test.Tests.Unit.DtoValidation
{
    public abstract class BaseDtoTest : BaseUnitTest
    {
        protected ValidationResult FindMember(IEnumerable<ValidationResult> validationResults, string memberNameToFind)
        {
            return validationResults
                  .FirstOrDefault(validationResult => validationResult.MemberNames.Any(
                      memberName => memberName.Equals(memberNameToFind)));
        }
    }
}
