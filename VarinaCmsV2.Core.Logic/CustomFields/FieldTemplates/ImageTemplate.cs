using DotLiquid;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates
{
    public class ImageTemplate:Drop
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public ResizedImageListLiquidData Resizeds { get; set; }
    }
    public class ResizedImageListLiquidData : Drop
    {
        public List<ImageData> Items { get; set; }
        public override object BeforeMethod(string method)
        {
            return Items.FirstOrDefault(x => x.SizeName == method)?.Path;
        }
    }
}