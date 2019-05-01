using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.ViewModel.Meta;

namespace VarinaCmsV2.ViewModel
{
    public interface IHaveMetaDataViewModel
    {
         List<MetaDataViewModel> MetaData { get; set; }
    }
}
