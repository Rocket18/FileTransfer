using _2c2p.application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        /// <summary>
        /// Upload file with max size 1 Mb
        /// </summary>
        /// <param name="file">IFormFile</param>
        /// <returns>IActionResult</returns>
        [Route("upload"), HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1048576)]
        [RequestSizeLimit(1048576)] // for Kestrel

        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var result =  await _fileImportService.Import(file);

            if (result.All(x => !x.IsError)) 
            {
                return Ok();
            }

            return BadRequest(result);
        }
    }
}
