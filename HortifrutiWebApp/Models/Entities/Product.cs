using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HortifrutiWebApp.Models.Entities
{
    public class Product
    {
        [Key]
        [Display(Name = "Code")]
        [DisplayFormat(DataFormatString = "{0:D6}")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
        [MaxLength(100, ErrorMessage = "O campo \"{0}\" deve conter até \"{1}\"  caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
        [MaxLength(500, ErrorMessage = "O campo \"{0}\" deve conter até \"{1}\" caracteres.")]
        public string Descrition { get; set; }

        [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
        public int?  Stock { get; set; }
    }
}
