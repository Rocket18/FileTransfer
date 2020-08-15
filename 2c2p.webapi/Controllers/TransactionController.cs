using _2c2p.application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _2c2p.webapi.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]

    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [Route("list"), HttpGet]

        public async Task<IActionResult> List()
            => Ok(await _transactionService.GetAllTransactions());

    }
}
