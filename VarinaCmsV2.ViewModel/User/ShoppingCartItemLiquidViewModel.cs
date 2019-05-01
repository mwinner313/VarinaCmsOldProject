using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.ViewModel.User
{
    public class ShoppingCartItemLiquidViewModel:Drop
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public  ProductLiquidAdapter Product { get; set; }
        public List<string> Warnings { get; set; }=new List<string>();
        public Guid ProductId { get; set; }
    }
}
