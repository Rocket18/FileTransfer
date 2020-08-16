using _2c2p.application.Contracts;
using _2c2p.domain.Entities;
using _2c2p.domain.Enumerations;
using _2c2p.domain.Models;
using _2c2p.infrastructure.Helpers;
using _2c2p.infrastructure.Models;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace _2c2p.infrastructure.Services
{
    public class CsvFileService : IFileService
    {
        public const string DateFormat = "dd/MM/yyyy hh:mm:ss";

        public async Task<List<TransactionModel>> ExportToModel(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var csvFile = await reader.ReadToEndAsync();

                var textReader = new StringReader(csvFile);

                var csv = new CsvReader(textReader, CultureInfo.InvariantCulture);

                csv.Configuration.HasHeaderRecord = false;

                csv.Configuration.MissingFieldFound = null;

                var transactions = new List<TransactionModel>();

                foreach (var item in csv.GetRecords<CsvTransactionData>())
                {
                    var t = new TransactionModel()
                    {
                        Id = item.Id,
                        Amount = item.Amount,
                        CurrencyCode = item.CurrencyCode
                    };

                    DateTime.TryParseExact(item.TransactionDate, DateFormat, null, System.Globalization.DateTimeStyles.None, out var date);

                    t.TransactionDate = date;

                    t.Status = StatusHelper.GetStatus(item.Status);

                    transactions.Add(t);
                }

                return transactions;
            }
        }
    }
}
