using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HortifrutiWebApp.Models.Entities
{
    [Owned]
    public class Address
    {
        [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
        [RegularExpression(@"[0-9]{8}$", ErrorMessage = "Formato inválido!")]
        [MaxLength(8, ErrorMessage = "O campo \"{0}\" deve conter no máximo \"{1}\" caracteres!")]
        [UIHint("_CepTemplate")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
        [MaxLength(10, ErrorMessage = "O campo \"{0}\" deve conter no máximo \"{1}\" caracteres!")]
        [Display(Name = "Número")]
        public string Number { get; set; }

        [Required(ErrorMessage = "O CEP informado não corresponde a um endereço válido")]
        [MaxLength(100, ErrorMessage = "O campo \"{0}\" deve conter no máximo \"{1}\" caracteres!")]
        [Display(Name = "Rua")]
        public string Street { get; set; }

        [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
        [MaxLength(80, ErrorMessage = "O campo \"{0}\" deve conter no máximo \"{1}\" caracteres!")]
        [Display(Name = "Bairro")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
        [MaxLength(80, ErrorMessage = "O campo \"{0}\" deve conter no máximo \"{1}\" caracteres!")]
        [Display(Name = "Cidade")]
        public string City { get; set; }

        [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
        [MaxLength(2, ErrorMessage = "O campo \"{0}\" deve conter no máximo \"{1}\" caracteres!")]
        [Display(Name = "Estado")]
        public string State { get; set; }

        [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
        [Display(Name = "Complemento")]
        public string Complement { get; set; }

        [MaxLength(100, ErrorMessage = "O campo \"{0}\" deve conter no máximo \"{1}\" caracteres!")]
        [Display(Name = "Referência")]
        public string Reference { get; set; }
    }
}