using _2c2p.domain.Enumerations;

namespace _2c2p.infrastructure.Helpers
{
    public class StatusHelper
    {
        public static TransactionStatus GetStatus(string status)
        {
            return status switch
            {
                "Approved" => TransactionStatus.Approved,
                "Failed" => TransactionStatus.Rejected,
                "Finished" => TransactionStatus.Done,
                _ => TransactionStatus.Done,
            };
        }
    }
}
