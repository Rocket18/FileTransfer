using _2c2p.application.Models;
using _2c2p.domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2c2p.application.Contracts
{
    public interface IValidationService
    {
       Task<ValidationResult> Validate(List<Transaction> data);
    }
}
