using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    public class CustomRangeNumberAttribute : ValidationAttribute
    {
        public double MinValue { get; set; } = double.MaxValue;
        public double MaxValue { get; set; } = double.MaxValue;
        public bool IsNullable { get; set; }

        public override bool IsValid(object value)
        {
            var valid = true;
            if (IsNullable && value == null) return true;
            else if (!IsNullable && value == null) return false;

            try
            {
                var newValue = Double.Parse(value.ToString());
                if (MinValue != double.MaxValue)
                    valid = valid && newValue >= MinValue;
                if (MaxValue != double.MaxValue)
                    valid = valid && newValue <= MaxValue;

            }
            catch (Exception e)
            {
                valid = false;
            }

            return valid; 
        }

        public override string FormatErrorMessage(string name)
        {
            if (MinValue != double.MaxValue && MaxValue != double.MaxValue)
            {
                return $@"NUMBER_BETWEEN ""{name.ToUpperInvariant()}|{MinValue}|{MaxValue}""";
            }
            else if (MinValue != double.MaxValue && MaxValue != double.MaxValue)
            {
                return $@"NUMBER_GREATER_OR_EQ ""{name.ToUpperInvariant()}|{MinValue}""";
            }
            else if (MinValue != double.MaxValue && MaxValue != double.MaxValue)
            {
                return $@"NUMBER_LESS_OR_EQ ""{name.ToUpperInvariant()}|{MaxValue}""";
            }

            return base.FormatErrorMessage(name);
        }
    }
}
