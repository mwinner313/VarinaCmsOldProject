using System;
using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class FieldDefenitionGroup:BaseEntity
    {
        public string Title { get; set; }
        public EntityScheme Scheme { get; set; }
        public Guid? SchemeId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? ProductCategoryId { get; set; }
        public Category Category { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ICollection<FieldDefenition> FieldDefenitions { get; set; }=new List<FieldDefenition>();
    }
}