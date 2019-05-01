using System;
using System.Collections.Generic;
using System.Linq;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Meta;

namespace VarinaCmsV2.ViewModel.Category
{
    public class CategoryLiquidViewModel : UrlableLiquidAdapter
    {
        public List<EntityLiquidAdapter> Entities { get; set; }
        public CategoryLiquidViewModel Parent { get; set; }
        public EntitySchemeViewModel Scheme { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int CurrentPage { get; set; }
        public int AllPagesCount { get; set; }
        public string DisplayName { get; set; }
        public List<MetaDataLiquidViewMdoel> Metas { get; set; } = new List<MetaDataLiquidViewMdoel>();

        public override object BeforeMethod(string method)
        {
            var metas = Metas.Where(x => x.MetaName == method).ToList();
            if (metas.Count == 0)
                return base.BeforeMethod(method);

            if (metas.Count == 1) return metas.First();

            return metas;
        }
        public int FivePreviewsPage
        {
            get
            {
                if (CurrentPage - 5 < 1) return 1;
                return CurrentPage - 5;
            }
        }
        public int FiveNextPage
        {
            get
            {
                if (CurrentPage + 5 > AllPagesCount) return AllPagesCount;
                return CurrentPage + 5;
            }
        }
    }
}
