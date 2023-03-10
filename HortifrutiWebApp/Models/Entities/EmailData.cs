using System;
using System.ComponentModel.DataAnnotations;

namespace HortifrutiWebApp.Models.Entities
{
    public class EmailData
    {
        [Required(ErrorMessage = "\"{0}\" obrigatório")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
