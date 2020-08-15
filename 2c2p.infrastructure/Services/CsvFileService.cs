using _2c2p.application.Contracts;
using _2c2p.domain.Entities;
using _2c2p.domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                string[] filelines = csvFile.Split(new string[] { "\r\n" }, StringSplitOptions.None).Skip(1).ToArray();

                filelines = filelines.Take(filelines.Count() - 1).ToArray();

                //          var xml = new XElement("TopElement", filelines.Select(line => new XElement("CsvItem",
                //line.Split(',').Select((column, index) => new XElement("Column" + index, column)))));

                //          records = xml.Descendants("CsvItem").Select(item => new Transaction()
                //          {
                //              or1 = (string)item.Element("Column0"),
                //              exitStatus = (string)item.Element("Column1"),
                //              vendorState = (string)item.Element("Column2")
                //          }).ToList();

                return new List<TransactionModel>();
            }


        }
    }
}
