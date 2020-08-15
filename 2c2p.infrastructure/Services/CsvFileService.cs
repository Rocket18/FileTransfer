using _2c2p.application.Contracts;
using _2c2p.domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2c2p.infrastructure.Services
{
    public class CsvFileService : IFileService
    {
        public Task<List<Transaction>> ExportToModel(IFormFile file)
        {
            throw new System.NotImplementedException();
        }
    }
}
