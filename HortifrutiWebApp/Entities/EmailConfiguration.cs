
namespace HortifrutiWebApp.Entities
{
    public class EmailConfiguration
    {
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Password { get; set; }
        public string ServerAddressEmail { get; set; }
        public string ServerPortEmail { get; set; }
        public bool UseSSL { get; set; }
    }
}