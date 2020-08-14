using System;

namespace _2c2p.domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public int CurrencyCode { get; set; }

        public DateTime TransactionDate { get; set; }

        public byte Status { get; set; }
    }
}
