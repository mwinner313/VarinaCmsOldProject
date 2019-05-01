using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Contracts.MetaData;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Logic.MetaData.LiquidAdapters
{
    public class ImageMetaLiquidAdapter : IMetaDataLiquidAdapter
    {
        private readonly List<string> _validTypes = new List<string>() { "image" };
        public object GetLiquidAdaptedData(JObject data)
        {
            var image = data.ToObject<ImageData>();
            return new
            {
                path = image.Path,
                width = image.Width,
                height = image.Height,
                sizeName = image.SizeName
            };
        }

        public bool IsValidFor(string metaType)
        {
            return _validTypes.Any(x => x == metaType);
        }
    }
}
