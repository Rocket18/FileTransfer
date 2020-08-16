using _2c2p.application.Contracts;
using _2c2p.application.Models;
using _2c2p.domain.Enumerations;
using _2c2p.domain.Models;
using _2c2p.persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2c2p.application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DiBiContext _context;

        public TransactionService(DiBiContext context)
        {
            _context = context;

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<List<TransactionViewModel>> GetAllTransactions(TransactionFilterModel filterModel)
        {
            var query = _context.Transactions.Include(i => i.CurrencyCode).Select(x => x);


            if (filterModel.Currency != default)
            {
                query = query.Where(x => x.CurrencyCodeId == filterModel.Currency);
            }


            if (filterModel.Status != TransactionStatus.None)
            {
                query = query.Where(x => x.Status == (byte)filterModel.Status);
            }

            if (filterModel.FromDate != default)
            {
                query = query.Where(x => x.TransactionDate >= filterModel.FromDate);
            }


            if (filterModel.ToDate != default)
            {
                query = query.Where(x => x.TransactionDate <= filterModel.ToDate);
            }

            var model = await query.ToListAsync();

            var list = new List<TransactionViewModel>();

            foreach (var item in model)
            {
                list.Add(new TransactionViewModel()
                {
                    Id = item.TransactionId,
                    Payment = $"{item.Amount.ToString("n2")} {item.CurrencyCode.Code}",
                    Status = ((TransactionStatus)Enum.ToObject(typeof(TransactionStatus), item.Status)).ToString().FirstOrDefault()
                });
            }

            return list;
        }
    }
}
