using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Services.DataServices
{
    public class ProductDataService : BaseDataService<Product>, IProductDataService
    {
        public ProductDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override Task<Product> GetAsync(Guid id)
        {
            return Query.Include(x => x.PrimaryCategory)
                .Include(x => x.AppliedDiscounts)
                .Include(x => x.Eshantions)
                .Include(x => x.RequiredProducts)
                .Include(x => x.CrossSellings)
                .Include(x => x.UpSellings)
                .Include(x => x.DeliveryDates)
                .Include(x => x.Pictures)
                .Include(x => x.ProductReviews)
                .Include(x => x.AssociatedProducts)
                .Include(x => x.File)
                .Include(x => x.SampleFile)
                .Include(x => x.Fields)
                .Include(x => x.RelatedCategories)
                .Include(x => x.Creator)
                .Include(x => x.Tags)
                .Include(x => x.Scheme)
                .Include(x => x.Fields.Select(f => f.FieldDefenition)).FirstAsync(x => x.Id == id);
        }
    }
}
