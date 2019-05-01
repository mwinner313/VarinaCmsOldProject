using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersianDate;
using VarinaCmsV2.ViewModel.Meta;

namespace VarinaCmsV2.ViewModel.FileData
{
    public class FileDataViewModelWithMeta :BaseVeiwModel, IHaveMetaDataViewModel
    {
        public List<MetaDataViewModel> MetaData { get; set; }=new List<MetaDataViewModel>();
        /// <summary>
        /// virtual path to file starts with / ex: /Uploads/test.png
        /// </summary>
        public string Path { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public string CreatorUserName { get; set; }
        public string CreatorName { get; set; }
        public long Size { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string UpdateDateTimeString => ConvertDate.ToFa(UpdateDateTime, "yyyy/MM/dd - hh:mm");
        public string CreateDateTimeString => ConvertDate.ToFa(CreateDateTime, "yyyy/MM/dd - hh:mm");
    }
}
