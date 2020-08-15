namespace _2c2p.webapi.Models
{
    public class CorsSettings
    {
        public string AllowedHosts { get; set; }

        public string[] Origins => AllowedHosts?.Split(",") ?? new string[0];
    }
}
