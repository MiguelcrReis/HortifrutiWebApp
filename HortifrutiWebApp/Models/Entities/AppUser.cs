using Microsoft.AspNetCore.Identity;

namespace HortifrutiWebApp.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string Nome { get; set; }
    }
}