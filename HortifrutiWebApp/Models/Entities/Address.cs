using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HortifrutiWebApp.Models.Entities
{
    [Owned]
    public class Address
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [RegularExpression(@"[0-9]{8}$", ErrorMessage = "Formato inválido!")]
        [MaxLength(8, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres!")]
        [UIHint("_CepTemplate")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [MaxLength(10, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres!")]
        public string Number { get; set; }

        [Required(ErrorMessage = "O CEP informado não corresponde a um endereço válido")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres!")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [MaxLength(80, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres!")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [MaxLength(80, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres!")]
        public string City { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [MaxLength(2, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres!")]
        public string State { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Complement { get; set; }

        [MaxLength(100, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres!")]
        public string Reference { get; set; }
    }
}
