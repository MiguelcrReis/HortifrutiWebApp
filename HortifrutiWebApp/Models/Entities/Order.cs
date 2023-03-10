using HortifrutiWebApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HortifrutiWebApp.Models.Entities
{
    public class Order
    {
        [Key]
        [Display(Name = "Código")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
        [Display(Name = "Data/Hora")]
        public DateTime DateTimeOrder { get; set; }

        [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Valor Total")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "\"{0}\" é obrigatório!")]
        [DisplayName("Status")]
        public OrderStatus OrderStatus { get; set; }

        public int? ClientId { get; set; }

        public string CartId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        public Address Address { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
