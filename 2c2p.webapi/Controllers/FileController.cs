using _2c2p.application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _2c2p.webapi.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class FileController : Controller
    {
        private readonly IFileImportService _fileImportService;

        public FileController(IFileImportService fileImportService)
        {
            _fileImportService = fileImportService;
        }

        [Route("file/upload"), HttpPost]

        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            await _fileImportService.Import(file);

            return Ok();
        }
        
    }
}
