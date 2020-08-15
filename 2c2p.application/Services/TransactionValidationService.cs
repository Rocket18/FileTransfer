using _2c2p.application.Contracts;
using _2c2p.application.Models;
using _2c2p.domain.Enumerations;
using _2c2p.domain.Models;

namespace _2c2p.application.Services
{
    public class TransactionValidationService : IValidationService<TransactionModel>
    {
        public ValidationResult<TransactionModel> Validate(TransactionModel transaction)
        {
            var result = new ValidationResult<TransactionModel>(transaction);

            if (string.IsNullOrEmpty(transaction.Id))
            {
                result.AddEmptyFieldError(nameof(transaction.Id));
            }
            else if (transaction.Id.Length > 50)
            {
                result.AddFieldError(nameof(transaction.Id), $"Transaction Identificator {transaction.Id} should be less than 50 length");
            }

            if (transaction.Amount == default)
            {
                result.AddEmptyFieldError(nameof(transaction.Amount));
            }

            if (string.IsNullOrEmpty(transaction.CurrencyCode))
            {
                result.AddEmptyFieldError(nameof(transaction.CurrencyCode));
            }

            if (transaction.Status == TransactionStatus.None)
            {
                result.AddEmptyFieldError(nameof(transaction.Status));
            }

            result.Message = result.IsError ? "Validation  didn’t pass" : "Validation pass";

            return result;
        }
    }
}
