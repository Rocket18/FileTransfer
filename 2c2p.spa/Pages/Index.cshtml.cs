using _2c2p.domain.Models;
using _2c2p.mvc.Providers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2c2p.mvc.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ApiProvider _apiProvider;
        public IList<TransactionViewModel> Transactions { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApiProvider apiProvider)
        {
            _logger = logger;

            _apiProvider = apiProvider;
        }

        public async Task OnGet()
        {
            Transactions = await _apiProvider.GetTransactions();
        }
    }
}
