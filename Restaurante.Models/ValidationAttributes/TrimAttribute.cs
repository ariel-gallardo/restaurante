using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    public class TrimAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string strValue)
            {

                var trimmedValue = strValue.Trim();

                var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(validationContext.ObjectInstance, trimmedValue);
                }
            }

            return ValidationResult.Success;
        }
    }

    public class TrimAllAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string strValue)
            {

                var trimmedValue = strValue.Trim().Replace(@" ", string.Empty);

                var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(validationContext.ObjectInstance, trimmedValue);
                }
            }

            return ValidationResult.Success;
        }
    }

}
