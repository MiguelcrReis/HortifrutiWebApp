using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HortifrutiWebApp.Models
{
    public class Product
    {
        [Key]
        public int IdProduct { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [MaxLength(1000)]
        public string Descrition { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "{0} required")]
        public int Stock { get; set; }
    }
}
