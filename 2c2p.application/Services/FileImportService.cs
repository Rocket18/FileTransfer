using _2c2p.application.Contracts;
using _2c2p.application.Enumerations;
using _2c2p.application.Helpers;
using _2c2p.application.Models;
using _2c2p.domain.Models;
using _2c2p.persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public FileImportService(DiBiContext context, Func<FileType, IFileService> importServiceResolver, IValidationService<TransactionModel> validationService)
        {
            _context = context;
            _importServiceResolver = importServiceResolver;
            _validationService = validationService;
        }

        public async Task<List<ValidationResult<TransactionModel>>> Import(IFormFile file)
        {
            if (file == null)
            {
                throw new Exception("No file to import");

            }

            var fileType = FileHelper.GetFileType(file.ContentType);

            var fileService = _importServiceResolver(fileType);

            if (fileService == null)
            {
                throw new Exception("Unknown format");
            }

            var model = await fileService.ExportToModel(file);

            var validationErrors = new List<ValidationResult<TransactionModel>>();

            foreach (var item in model)
            {
                validationErrors.Add(_validationService.Validate(item));
            }

            if (!validationErrors.All(x => !x.IsError))
            {
                await InsertUpdateTransactions(model);
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

            _context.AddRange(newTransactions);

            await _context.SaveChangesAsync();
        }
    }
}
