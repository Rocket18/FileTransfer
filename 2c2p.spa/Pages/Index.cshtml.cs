using _2c2p.domain.Enumerations;
using _2c2p.domain.Models;
using _2c2p.mvc.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2c2p.mvc.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ApiProvider _apiProvider;
        public IList<TransactionViewModel> Transactions { get; set; }

        [FromQuery(Name = "currency")]
        public int Currency { get; set; }

        [FromQuery(Name = "status")]
        public int Status { get; set; }


        [FromQuery(Name = "from")]
        public DateTime? FromDate { get; set; }


        [FromQuery(Name = "to")]
        public DateTime? ToDate { get; set; }


        public List<SelectListItem> StatusList { get; private set; }

        public List<SelectListItem> CurrencyList { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, ApiProvider apiProvider)
        {
            _logger = logger;

            _apiProvider = apiProvider; 
        }

        public async Task OnGet()
        {
            Transactions = await _apiProvider.GetTransactions(Currency, Status, FromDate, ToDate);

            CurrencyList = (await _apiProvider.GetCurrencyList()).Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Code
            }).ToList();

            StatusList = Enum.GetValues(typeof(TransactionStatus))
                           .Cast<TransactionStatus>()
                           .Skip(1)
                           .Select(t => new SelectListItem
                           {
                               Value = ((int)t).ToString(),
                               Text = t.ToString()
                           })
                           .ToList();
        }
    }
}
