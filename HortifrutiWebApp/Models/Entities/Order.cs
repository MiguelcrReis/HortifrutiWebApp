﻿using HortifrutiWebApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HortifrutiWebApp.Models.Entities
{
    public class Order
    {
        [Key]
        [Display(Name = "Code")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Display(Name = "Data/Hora")]
        public DateTime DateTimeOrder { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [DisplayName("Order status")]
        public OrderStatus OrderStatus { get; set; }

        public int ClientId { get; set;  }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        public Address Address { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
