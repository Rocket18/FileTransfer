using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _2c2p.webapi.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]

    public class TransactionController : Controller
    {

        //[Route("list"), HttpGet]

        //public async Task<IActionResult> List()
        //{

        //}

        //[Route("file/upload"), HttpPost]

        //public async Task<IActionResult> UploadFile()
        //{

        //}
    }
}
