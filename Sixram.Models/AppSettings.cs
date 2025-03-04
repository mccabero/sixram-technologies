namespace Sixram.Models
{
    public class AppSettings
    {
        public string Secret { get; set; } = string.Empty;

        public string ApiUrl { get; set; } = string.Empty;

        public string ApiIssuer { get; set; } = string.Empty;

        public int TokenExpirationInDays { get; set; }

        public string ClientUrl { get; set; } = string.Empty;
    }
}
