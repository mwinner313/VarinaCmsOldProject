using System;
using System.ComponentModel.DataAnnotations;
using VarinaCmsV2.Common.ValidationAttributes;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Cart
{
    public class ShoppingCartItemUpdateModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MinValue(0)]
        public int Quantity { get; set; }
    }
}