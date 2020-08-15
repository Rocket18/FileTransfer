using _2c2p.application.Contracts;
using _2c2p.application.Models;
using _2c2p.persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            var model = await _context.Transactions.ToListAsync();

            // map vm

            return new List<TransactionViewModel>();
        }
    }
}
