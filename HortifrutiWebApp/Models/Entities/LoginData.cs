using System;
using System.ComponentModel.DataAnnotations;

namespace HortifrutiWebApp.Models.Entities
{
    public class LoginData
    {
        [Required(ErrorMessage = "\"{0}\" obrigatório.")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "\"{0}\" obrigatório.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar de mim")]
        public bool Remember { get; set; }
    }
}
