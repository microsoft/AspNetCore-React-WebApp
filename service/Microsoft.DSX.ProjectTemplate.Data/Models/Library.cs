using System.ComponentModel.DataAnnotations;

namespace Microsoft.DSX.ProjectTemplate.Data.Models
{
    public class Library : AuditModel<int>
    {
        [MaxLength(Constants.MaximumLengths.StringColumn)]
        public string Name { get; set; }

        public Address Address { get; set; }
    }
}
