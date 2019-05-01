using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.Logic.Templating.Liquid.Blocks
{
    public class ProductPriceCalculator:Tag
    {
        private string _product = string.Empty;
        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            var keys = markup.Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            try
            {
                _product = keys[0];
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($" ProductPriceCalculator {markup}");
            }
            base.Initialize(tagName, markup, tokens);
        }

        public override void Render(Context context, TextWriter result)
        {
            var product = context[_product] as ProductLiquidAdapter;
            if(product is null) return;

            if(!product.CanAddToCart)result.Write("ناموجود");
        }
    }
}
