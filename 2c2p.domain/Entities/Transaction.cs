using System;
using System.ComponentModel.DataAnnotations;

namespace _2c2p.domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }

        [MaxLength(40)]
        public string TransactionId { get; set; }

        public int CurrencyCodeId { get; set; }

        public CurrencyCode CurrencyCode { get; set; }

        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        public byte Status { get; set; }
    }
}
