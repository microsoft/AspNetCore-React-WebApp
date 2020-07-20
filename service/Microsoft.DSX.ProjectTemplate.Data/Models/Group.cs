using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.DSX.ProjectTemplate.Data.Models
{
    public class Group : AuditModel<int>
    {
        [Required]
        [MaxLength(Constants.MaximumLengths.StringColumn)]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();

        public virtual Library DefaultLibrary { get; set; }
    }
}
