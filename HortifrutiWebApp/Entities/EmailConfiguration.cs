using System;

namespace HortifrutiWebApp.Entities
{
    public class EmailConfiguration
    {
        #region Parameters
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Password { get; set; }
        public string ServerAddressEmail { get; set; }
        public int ServerPortEmail { get; set; }
        public bool UseSSL { get; set; }
        #endregion
    }
}