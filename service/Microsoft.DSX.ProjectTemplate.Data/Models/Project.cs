using System.ComponentModel.DataAnnotations;

namespace Microsoft.DSX.ProjectTemplate.Data.Models
{
    public class Project : AuditModel<int>
    {
        [Required]
        [MaxLength(Constants.MaximumLengths.StringColumn)]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public virtual Group Group { get; set; }

        public int GroupId { get; set; }

        public virtual User Owner { get; set; }
    }
}
