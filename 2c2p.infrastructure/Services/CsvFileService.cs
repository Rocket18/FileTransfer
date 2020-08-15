using _2c2p.application.Contracts;
using _2c2p.domain.Entities;
using _2c2p.domain.Models;
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
        public async Task<List<TransactionModel>> ExportToModel(IFormFile file)
        {

            var records = new List<Transaction>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var csvFile = await reader.ReadToEndAsync();

                var textReader = new StringReader(csvFile);

                var csv = new CsvReader(textReader, CultureInfo.InvariantCulture);

                csv.Configuration.HasHeaderRecord = false;

                csv.Configuration.MissingFieldFound = null;

                var data = csv.GetRecords<CsvTransactionData>();

                return new List<TransactionModel>();
            }


        }
     
    }
}
