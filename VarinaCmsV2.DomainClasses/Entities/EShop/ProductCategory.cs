using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;

namespace VarinaCmsV2.DomainClasses.Entities.EShop
{
    public class ProductCategory : BaseEntity, IScheme
        , ITemplatedItem,
        IHaveMetaData,
        IParentChildUrlChainedItem<ProductCategory>,
        IAccessRestrictedItem,
        ISoftDeletibleItem,
        IUrlable, ICategory
    {
        public static string MetaTypeName= nameof(ProductCategory);
        public virtual ProductCategory Parent { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
      
        public string UrlSegment { get; set; }
        public CategoryType CategoryType { get; set; }
        public ICollection<FieldDefenitionGroup> FieldDefenitionGroups { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public string Handle { get; set; }
        public string Title { get; set; }
        public string AllowedRolesString { get; set; }
        public AccessType AccessType { get; set; }
        public bool IsDeleted { get; set; }
        public EntityScheme Scheme { get; set; }
        public Guid? SchemeId { get; set; }
        [NotMapped]
        public ObservableCollection<AllowedRole> AllowedRoles { get; set; }
        public virtual ICollection<ProductCategory> Childs { get; set; }
        public ICollection<Discount> AppliedDiscounts { get; set; }
        public ICollection<Product> Products { get; set; }
        public virtual ICollection<FieldDefenition> FieldDefenitions { get; set; }
    }
}
