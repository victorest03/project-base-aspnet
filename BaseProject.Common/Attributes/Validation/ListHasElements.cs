using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Common.Attributes.Validation
{
    public class ListHasElements : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is IList list))
                return new ValidationResult(null);
            return list.Count == 0 ? new ValidationResult(null) : null;
        }
    }
}
