using System;
using System.ComponentModel.DataAnnotations;

namespace _2c2p.domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public int CurrencyCodeId { get; set; }

        public CurrencyCode CurrencyCode { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        public byte Status { get; set; }
    }
}
