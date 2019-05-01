using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid.Tags;
using EntityFramework.DynamicFilters;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core
{
    public class GlobalDataOrdering:GlobalFilter
    {
        public override void ApplyFilter(DbModelBuilder modelBuilder)
        {
        }
    }
}
