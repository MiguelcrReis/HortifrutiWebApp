using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HortifrutiWebApp.Models.Entities
{
    public class OrderItem
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
        [Display(Name = "Quantidade")]
        public float Quantity { get; set; }

        [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Valor Unitário")]
        public decimal UnitaryValue { get; set; }

        [NotMapped]
        public decimal ItemValue
        {
            get
            {
                return decimal.Multiply(decimal.Parse(Quantity.ToString()), UnitaryValue);
            }
        }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
