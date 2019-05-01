using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.ViewModel
{
    public interface IEntityViewModel
    {
         List<FieldAddOrUpdateViewModel> Fields { get; set; }
    }
}
