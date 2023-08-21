using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BettingOddsCalculator.Validations
{
    public class AmericanOddsRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int intValue && value is not null)
            {
                if (intValue >= 100 || intValue <= -100)
                {
                    return ValidationResult.Success!;
                }
            }

            return new ValidationResult("The field American Odds must be either greater than or equal to 100 or less than or equal to -100.");
        }
    }
}