using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace _2c2p.application.Contracts
{
    public interface IFileImportService
    {
        Task Import(IFormFile file);
    }
}
