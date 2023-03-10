using System;
using System.ComponentModel.DataAnnotations;

namespace HortifrutiWebApp.Models.Entities
{
    public class Passwords
    {
        [Required(ErrorMessage = "\"{0}\" obrigatório.")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "A \"{0}\" deve conter pelo menos \"{2}\" e no máximo  \"{1}\" caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "\"{0}\" obrigatório.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmarção de Senha")]
        [Compare("Password", ErrorMessage = "A confirmação de senha deve ser igual a senha.")]
        public string ConfirmationPassword { get; set; }
    }
}
