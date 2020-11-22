using HireRank.Common.ModelValidators;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ValidationException = HireRank.Common.Exceptions.ValidationException;

namespace HireRank.Common.ExceptionBuilders
{
    public static class ValidationExceptionBuilder
    {
        public static ValidationException BuildValidationException(ValidationResults validationResults)
        {
            var errorMessages = validationResults.ValidationResultsMessages;
            StringBuilder summaryErrorMessage = new StringBuilder();
            foreach (ValidationResult result in errorMessages)
            {
                summaryErrorMessage.Append(result.ErrorMessage);
            }
            return new ValidationException(summaryErrorMessage.ToString());
        }
    }
}
