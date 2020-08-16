using _2c2p.application.Models;
using _2c2p.domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2c2p.application.Contracts
{
    public interface ITransactionService
    {
        Task<List<TransactionViewModel>> GetAllTransactions(TransactionFilterModel model);
    }
}
