using _2c2p.application.Contracts;
using _2c2p.application.Models;
using _2c2p.persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace _2c2p.webapi.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]

    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly DiBiContext _context;

        public TransactionController(ITransactionService transactionService, DiBiContext context)
        {
            _transactionService = transactionService;

            _context = context;
        }

        /// <summary>
        /// Get transaction list
        /// </summary>
        /// <returns>List<TransactionViewModel></returns>
        [Route("list"), HttpGet]

        public async Task<IActionResult> List([FromQuery]TransactionFilterModel model)
            => Ok(await _transactionService.GetAllTransactions(model));


        /// <summary>
        /// Get currency list
        /// </summary>
        /// <returns>List<CurrencyCode></returns>
        [Route("currency-list"), HttpGet]
        public async Task<IActionResult> CurrencyList()
          => Ok(await _context.CurrencyCodes.ToListAsync());

    }
}
