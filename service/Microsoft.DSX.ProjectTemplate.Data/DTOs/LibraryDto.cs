using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.DSX.ProjectTemplate.Data.DTOs
{
    public class LibraryDto : AuditDto<int>
    {
        public string Name { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                if (Name.Length > Constants.MaximumLengths.StringColumn)
                {
                    yield return new ValidationResult($"{nameof(Name)} must be less than {Constants.MaximumLengths.StringColumn} characters.", new[] { nameof(Name) });
                }
            }
            else
            {
                yield return new ValidationResult($"{nameof(Name)} cannot be null or empty.", new[] { nameof(Name) });
            }
        }
    }
}
