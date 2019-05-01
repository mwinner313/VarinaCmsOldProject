using System;
using DotLiquid;
using System.Collections.Generic;
using System.Linq;

namespace VarinaCmsV2.Common
{
    public class ImageData
    { 
        public ImageData()
        {
        }

        public string Path { get; set; }
        public string SizeName { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Name { get; set; }
    }
   
}