using _2c2p.domain.Enumerations;
using System;

namespace _2c2p.domain.Models
{
    public class TransactionModel
    {
        public string Id { get; set; }

        public string CurrencyCode { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public TransactionStatus Status { get; set; }
    }
}
