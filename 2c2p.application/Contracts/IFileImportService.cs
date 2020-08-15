using _2c2p.application.Models;
using _2c2p.domain.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2c2p.application.Contracts
{
    public interface IFileImportService
    {
        Task<List<ValidationResult<TransactionModel>>>Import(IFormFile file);
    }
}
