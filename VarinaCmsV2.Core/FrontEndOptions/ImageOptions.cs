using System.Collections.Generic;

namespace VarinaCmsV2.Core.FrontEndOptions
{
    public class ImageOptions
    {
        public List<string> AllowedTypes { get; set; }
        public List<ResizeOption> ResizeOptions{ get; set; }
    }
}