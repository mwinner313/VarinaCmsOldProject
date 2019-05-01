using System.Data.Entity;
using EntityFramework.DynamicFilters;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Infrastructure.GlobalFilters
{
    public class SoftDeletibleItemGlobalFilter:GlobalFilter
    {

        public override void ApplyFilter(DbModelBuilder modelBuilder)
        {
         modelBuilder.Filter("Soft_Delete",(ISoftDeletibleItem item)=>!item.IsDeleted);
        }
    }
}
