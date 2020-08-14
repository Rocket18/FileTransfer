using System.ComponentModel.DataAnnotations;

namespace _2c2p.domain.Entities
{
    public class CurrencyCode
    {
        public int Id { get; set; }

        [Required, MaxLength(3)]
        public string Code { get; set; }
    }
}
