using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace VarinaCmsV2.AutoMapperProfiles.Reslovers
{
    public class RowVersionConverterProfile : Profile
    {
        public RowVersionConverterProfile()
        {
            CreateMap<byte[], string>().ConvertUsing((x, y) =>
            {
                var strBuilder=new StringBuilder();
                foreach (byte t in x)
                {
                    strBuilder.Append(t+",");
                }
                return strBuilder.ToString();
            });
            CreateMap<string, byte[]>().ConvertUsing((str, bytes) =>
            {
                List<byte> retBytes=new List<byte>();
                var spliteds = str.Split(',').ToList();
                foreach (var splited in spliteds.ToList())
                {
                    if (string.IsNullOrEmpty(splited)) spliteds.Remove(splited);
                }
                foreach (var s in spliteds)
                {
                    retBytes.Add(byte.Parse(s));
                }
                return retBytes.ToArray();
            });
        }
    }
}
