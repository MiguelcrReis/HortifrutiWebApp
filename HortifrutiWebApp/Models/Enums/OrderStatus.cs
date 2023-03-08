using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HortifrutiWebApp.Models.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Carrinho de Compras")]
        ShoppingCart,
        [Display(Name = "Cancelado")]
        Canceled,
        [Display(Name = "Realizado")]
        Realized,
        [Display(Name = "Verificado")]
        Verified,
        [Display(Name = "Atendido")]
        Answered,
        [Display(Name = "Entregue")]
        Delivered
    }
}
