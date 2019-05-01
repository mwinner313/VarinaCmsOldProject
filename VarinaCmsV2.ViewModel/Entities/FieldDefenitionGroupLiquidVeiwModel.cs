using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;

namespace VarinaCmsV2.ViewModel.Entities
{
    public class FieldDefenitionGroupLiquidVeiwModel : Drop 
    {
        public Guid Id { get; set; }
        public List<FieldDefenitionLiquidVeiwModel> FieldDefenitions { get; set; }
        public string Title { get; set; }
    }
}
