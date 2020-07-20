using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.DSX.ProjectTemplate.Data.DTOs
{
    public abstract class BaseDto<TType> : IValidatableObject
    {
        public TType Id { get; set; }

        protected ModelStateDictionary ModelState { get; } = new ModelStateDictionary();

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}
