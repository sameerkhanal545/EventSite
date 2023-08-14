using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EventSite.Areas.Admin.Models
{
    public class RequiredImageAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null || file.Length == 0)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
