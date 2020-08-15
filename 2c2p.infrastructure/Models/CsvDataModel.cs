using CsvHelper.Configuration.Attributes;

namespace _2c2p.infrastructure.Models
{

    public class CsvTransactionData
    {
        [Index(0)]
        public string Id { get; set; }

        [Index(1)]
        public string Amount { get; set; }

        [Index(2)]
        public string CurrencyCode { get; set; }

        [Index(3)]
        public string TransactionDate { get; set; }

        [Index(4)]
        public string Status { get; set; }  
    }
}
