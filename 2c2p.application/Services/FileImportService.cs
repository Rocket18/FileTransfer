using _2c2p.application.Contracts;
using _2c2p.application.Enumerations;
using _2c2p.application.Helpers;
using _2c2p.application.Models;
using _2c2p.domain.Entities;
using _2c2p.domain.Models;
using _2c2p.persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2c2p.application.Services
{
    public class FileImportService : IFileImportService
    {
        private readonly DiBiContext _context;

        private readonly Func<FileType, IFileService> _importServiceResolver;

        private readonly IValidationService<TransactionModel> _validationService;

        private readonly ILogger<FileImportService> _logger;

        public FileImportService(
            DiBiContext context, Func<FileType, IFileService> importServiceResolver, 
            IValidationService<TransactionModel> validationService,
            ILogger<FileImportService> logger)
        {
            _context = context;
            _importServiceResolver = importServiceResolver;
            _validationService = validationService;
            _logger = logger;
        }

        public async Task<List<ValidationResult<TransactionModel>>> Import(IFormFile file)
        {
            if (file == null)
            {
                throw new Exception("No file to import");

            }

            var fileType = FileHelper.GetFileType(file.ContentType, file.FileName);

            var fileService = _importServiceResolver(fileType);

            if (fileService == null)
            {
                throw new Exception("Unknown format");
            }

            var model = await fileService.ExportToModel(file);

            var validationErrors = new List<ValidationResult<TransactionModel>>();

            foreach (var item in model)
            {
                var result = _validationService.Validate(item);
                if (result.IsError) 
                {
                    validationErrors.Add(result);
                }
            }

            if (validationErrors.All(x => !x.IsError))
            {
                await InsertUpdateTransactions(model);
            }
            else 
            {
                var jsonErrors = JsonConvert.SerializeObject(validationErrors);

                _logger.LogError($"Validation  didn’t pass for {fileType} {Environment.NewLine} {jsonErrors}");
            }

            return validationErrors;
        }

        private async Task InsertUpdateTransactions(List<TransactionModel> model)
        {
            var currencyCodes = await _context.CurrencyCodes.ToListAsync();

            var invoiceIds = model.Select(x => x.Id);

            var existingTransactions = await _context.Transactions.Where(x => invoiceIds.Contains(x.TransactionId)).ToListAsync();

            foreach (var item in existingTransactions)
            {
                var transaction = model.FirstOrDefault(x => x.Id == item.TransactionId);

                item.TransactionDate = transaction.TransactionDate;
                item.Amount = transaction.Amount;
                item.CurrencyCode = item.CurrencyCode;
                item.Status = item.Status;

                _context.Update(item);
            }

            var newTransactions = model.Where(x => !existingTransactions.Select(x => x.TransactionId).Contains(x.Id));

            foreach (var item in newTransactions)
            {
                var transaction = new Transaction()
                {
                    TransactionId = item.Id,
                    TransactionDate = item.TransactionDate,
                    Amount = item.Amount,
                    Status = (byte)item.Status
                };

                var currencyId = currencyCodes.FirstOrDefault(x => x.Code == item.CurrencyCode)?.Id;

                if (currencyId == null)
                {
                    throw new Exception($"Unsupported currency code {item.CurrencyCode} found");
                }
                else 
                {
                    transaction.CurrencyCodeId = currencyId.Value;
                }

                _context.Transactions.Add(transaction);
            }

            await _context.SaveChangesAsync();
        }
    }
}
