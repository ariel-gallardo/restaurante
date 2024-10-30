using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{

    public class ComparePasswordAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public ComparePasswordAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = value as string;
            var comparisonValue = validationContext.ObjectType
                .GetProperty(_comparisonProperty)
                ?.GetValue(validationContext.ObjectInstance) as string;

            if (currentValue != comparisonValue)
            {
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} and {_comparisonProperty} do not match.");
            }

            return ValidationResult.Success;
        }
    }

}
