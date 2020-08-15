using _2c2p.application.Models;

namespace _2c2p.application.Contracts
{
    public interface IValidationService<T>
    {
        ValidationResult<T> Validate(T data);
    }
}
