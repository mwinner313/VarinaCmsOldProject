using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.FileManager;
using VarinaCmsV2.ViewModel.Meta;

namespace VarinaCmsV2.ViewModel.FileData
{
    public class FileDataAddOrUpdateVeiwModel : BaseVeiwModel
    {
        public string Name { get; set; }

        public ActionPrompt ActionPrompt { get; set; }
        public List<MetaDataAddOrUpdateViewModel> MetaDatas { get; set; } = new List<MetaDataAddOrUpdateViewModel>();

    }
}
