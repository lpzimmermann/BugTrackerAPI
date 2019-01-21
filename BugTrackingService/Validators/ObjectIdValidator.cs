using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace BugTrackingService.Validators
{
    public class ObjectIdValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var parsed = ObjectId.TryParse((string) value, out var productId);

            if (!parsed)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return $"Id must be in the correct format.";
        }
    }
}