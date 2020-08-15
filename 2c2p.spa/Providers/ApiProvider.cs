using _2c2p.domain.Models;
using _2c2p.mvc.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace _2c2p.mvc.Providers
{
    /// <summary>
    /// Http  provider to get data from web API (Easy and better than AJAX call from html)
    /// </summary>
    public class ApiProvider
    {
        public const string TransactionList = "/api/transaction/list";

        private readonly IHttpClientFactory _clientFactory;
        private readonly CommonAppSettings _settings;

        public ApiProvider(IHttpClientFactory clientFactory, IOptions<CommonAppSettings> options)
        {
            _clientFactory = clientFactory;
            _settings = options.Value;
        }

        public async Task<List<TransactionViewModel>> GetTransactions()
        {
            try
            {
                using var http = _clientFactory.CreateClient();

                http.BaseAddress = new Uri(_settings.ApiUrl);

                var url = $"{TransactionList}"; // + sort

                var contentStream = await http.GetStreamAsync(url);

                using var streamReader = new StreamReader(contentStream);

                using var jsonReader = new JsonTextReader(streamReader);

                return new JsonSerializer().Deserialize<List<TransactionViewModel>>(jsonReader);

            }
            catch (Exception ex)
            {
                // log 

                return new List<TransactionViewModel>();
            }
        }

    }
}
