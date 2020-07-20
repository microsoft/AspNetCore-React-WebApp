using System.ComponentModel.DataAnnotations;

namespace Microsoft.DSX.ProjectTemplate.Data.Models
{
    public class Team : AuditModel<int>
    {
        [MaxLength(Constants.MaximumLengths.StringColumn)]
        public string Name { get; set; }

        public virtual Group Group { get; set; }

        public int GroupId { get; set; }
    }
}
