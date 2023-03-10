using System;
using System.ComponentModel.DataAnnotations;

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
