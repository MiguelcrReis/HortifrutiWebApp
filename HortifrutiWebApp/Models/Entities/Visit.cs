using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HortifrutiWebApp.Models.Entities
{
    public class Visit
    {
        [Required]
        public int ClientId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
