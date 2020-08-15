using _2c2p.application.Contracts;
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

        public async Task<List<TransactionViewModel>> GetAllTransactions()
        {
            var model = await _context.Transactions.Include(x=>x.CurrencyCode).ToListAsync();

            var list = new List<TransactionViewModel>();

            foreach (var item in model)
            {
                list.Add(new TransactionViewModel()
                {
                    Id = item.TransactionId,
                    Payment = $"{item.Amount.ToString("n2")} {item.CurrencyCode.Code}",
                    Status  = ((TransactionStatus)Enum.ToObject(typeof(TransactionStatus), item.Status)).ToString().FirstOrDefault()
                });
            }

            return list;
        }
    }
}
