using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.DSX.ProjectTemplate.Data.Models
{
    [Owned]
    public class Address
    {
        [MaxLength(Constants.MaximumLengths.StringColumn)]
        public string LocationAddressLine1 { get; set; }

        [MaxLength(Constants.MaximumLengths.StringColumn)]
        public string LocationAddressLine2 { get; set; }

        [MaxLength(Constants.MaximumLengths.StringColumn)]
        public string LocationCity { get; set; }

        [MaxLength(Constants.MaximumLengths.StringColumn)]
        public string LocationStateProvince { get; set; }

        [MaxLength(Constants.MaximumLengths.StringColumn)]
        public string LocationZipCode { get; set; }

        [MaxLength(Constants.MaximumLengths.StringColumn)]
        public string LocationCountry { get; set; }
    }
}
