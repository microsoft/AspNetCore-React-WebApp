using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.DSX.ProjectTemplate.Data.Utilities;

namespace Microsoft.DSX.ProjectTemplate.Test.Tests.Integration.Group
{
    public abstract class BaseGroupTest : BaseIntegrationTest
    {
        protected GroupDto GetGroupDto()
        {
            return new GroupDto()
            {
                Name = RandomFactory.GetCompanyName(),
                IsActive = RandomFactory.GetBoolean()
            };
        }
    }
}
