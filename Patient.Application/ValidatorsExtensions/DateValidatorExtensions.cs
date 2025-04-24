using FluentValidation;

namespace Patient.Application.ValidatorsExtensions
{
    public static class DateValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> IsDateTime<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(e => DateTime.TryParse(e.Substring(2), out _)).WithErrorCode("Invalid date format. For example, the date should be gt2025-04-21 12:41:42.5650000");
        }
    }
}
