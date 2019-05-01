using System;
using System.Collections.Generic;
using VarinaCmsV2.ViewModel.FileData;

namespace VarinaCmsV2.Core.Services.File
{
    public class GroupedByDateFile
    {
        public List<FileDataViewModelWithMeta> Files { get; set; }
        public string DateTime { get; set; }
   
    }
}