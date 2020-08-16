using _2c2p.application.Contracts;
using _2c2p.domain.Enumerations;
using _2c2p.domain.Models;
using _2c2p.infrastructure.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _2c2p.infrastructure.Services
{
    public class XmlFileService : IFileService
    {

        public Task<List<TransactionModel>> ExportToModel(IFormFile file)
        {
            var serializer = new XmlSerializer(typeof(XmlDataModel));

            XmlDataModel xmlModel;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                xmlModel = (XmlDataModel)serializer.Deserialize(reader);
            }

            var transactions = new List<TransactionModel>();

            foreach (var item in xmlModel.Transaction)
            {
                var t = new TransactionModel()
                {
                    Id = item.Id,
                    Amount = item.PaymentDetails.Amount,
                    CurrencyCode = item.PaymentDetails.CurrencyCode
                };
                DateTime.TryParse(item.TransactionDate, out var date);

                t.TransactionDate = date;

                TransactionStatus status;

                if (Enum.TryParse(item.Status, out status))
                {
                    t.Status = status;
                }

                transactions.Add(t);
            }

            return Task.FromResult(transactions);
        }
    }

}
