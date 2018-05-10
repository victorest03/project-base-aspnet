using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Model;

namespace Common.Extensions
{
    public static class ModelStateExtension
    {
        public static IEnumerable<Error> AllErrors(this ModelStateDictionary modelState)
        {
            var result = from ms in modelState
                where ms.Value.Errors.Any()
                let fieldKey = ms.Key
                let errors = ms.Value.Errors
                from error in errors
                select new Error(fieldKey, error.ErrorMessage);

            return result;
        }
    }
}
