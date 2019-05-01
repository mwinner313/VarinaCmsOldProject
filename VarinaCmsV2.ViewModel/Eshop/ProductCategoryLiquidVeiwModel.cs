using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Meta;

namespace VarinaCmsV2.ViewModel.Eshop
{
    public class ProductCategoryLiquidVeiwModel: PaginatedUrlableLiquidAdapter
    {
        public List<ProductLiquidAdapter> Products { get; set; }
        public string Title { get; set; }
        public string  DisplayName { get; set; }
        public ProductCategoryLiquidVeiwModel Parent { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public List<MetaDataLiquidViewMdoel> Metas { get; set; } = new List<MetaDataLiquidViewMdoel>();
        public List<FieldDefenitionLiquidVeiwModel> FieldDefenitions { get; set; }
        public List<FieldDefenitionGroupLiquidVeiwModel> FieldDefenitionGroups { get; set; }
        public override object BeforeMethod(string method)
        {
            var metas = Metas.Where(x => x.MetaName == method).ToList();
            if (metas.Count == 0)
                return base.BeforeMethod(method);

            if (metas.Count == 1) return metas.First();
            return metas;
        }
    }
}