using _2c2p.domain.Enumerations;
using System;

namespace _2c2p.application.Models
{
    public class TransactionFilterModel
    {
        public int Currency { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public TransactionStatus Status { get; set; }
    }
}
