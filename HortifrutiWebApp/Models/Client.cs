using System;
using System.ComponentModel.DataAnnotations;

namespace HortifrutiWebApp.Models
{
    public class Client
    {
        [Key]
        public int IdClient { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} o tamanho deve estar entre {2} e {1}")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [MaxLength(11, ErrorMessage = "O campo {0} deve conter {1} caracteres")]
        [RegularExpression(@"[0-9]{11}$", ErrorMessage = "O campo {0} deve conter 11 dígitos")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [EmailAddress(ErrorMessage = "Insira um endereço de {0} válido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contato Telêfonico")]
        public string Phone { get; set; }

    }
}
