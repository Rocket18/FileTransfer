using _2c2p.domain.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2c2p.application.Contracts
{
    public interface IFileService
    {
        Task<List<TransactionModel>> ExportToModel(IFormFile file);
    }
}
