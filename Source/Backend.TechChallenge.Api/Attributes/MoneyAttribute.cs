using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Backend.TechChallenge.Api.Attributes;

public class MoneyAttribute : ValidationAttribute
{
    private new const string ErrorMessage = "The field {0} must be a positive decimal number.";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
            return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));

        var stringValue = value.ToString()?.Replace(',', '.');

        if (!decimal.TryParse(stringValue, NumberStyles.Number, CultureInfo.InvariantCulture, out var result) || result <= 0)
            return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));

        return ValidationResult.Success;
    }
}
