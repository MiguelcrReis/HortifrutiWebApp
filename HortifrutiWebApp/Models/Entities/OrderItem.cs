﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HortifrutiWebApp.Models.Entities
{
    public class OrderItem
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        public float Quantity { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Unitary value")]
        public decimal UnitaryValue { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}