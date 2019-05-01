using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Common.ValidationAttributes;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Meta;

namespace VarinaCmsV2.ViewModel.Eshop
{
    public class ProductCategoryAddOrUpdateModel:BaseVeiwModel
    {
        public List<FieldDefenitionAddOrUpdateModel> FieldDefenitions { get; set; }
        public List<FieldDefenitionGroupAddOrUpdateModel> FieldDefenitionGroups { get; set; }
        public List<MetaDataAddOrUpdateViewModel> MetaData { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlSegment { get; set; }
        public int Order { get; set; }
        [NoSpace(ErrorMessage = "handle should not have space character")]
        public string Handle { get; set; }
        public string Title { get; set; }
        public Guid? ParentId { get; set; }
        public CategoryType CategoryType { get; set; }
    }
}
