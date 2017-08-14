using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Common.Attributes.Validation
{
    public class ListHasElements : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as IList;
            if (list == null)
                return new ValidationResult(null);
            return list.Count == 0 ? new ValidationResult(null) : null;
        }
    }
}
