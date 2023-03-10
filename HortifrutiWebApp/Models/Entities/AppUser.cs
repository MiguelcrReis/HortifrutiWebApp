using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HortifrutiWebApp.Models.Entities
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "Nome")]
        public string Name { get; set; }
    }
}