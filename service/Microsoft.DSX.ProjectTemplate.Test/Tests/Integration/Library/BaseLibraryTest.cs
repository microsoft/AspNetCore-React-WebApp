using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.DSX.ProjectTemplate.Data.Utilities;

namespace Microsoft.DSX.ProjectTemplate.Test.Tests.Integration.Library
{
    public abstract class BaseLibraryTest : BaseIntegrationTest
    {
        protected static LibraryDto GetLibraryDto()
        {
            return new LibraryDto()
            {
                Name = RandomFactory.GetCompanyName(),
                

            };
        }
    }
}
