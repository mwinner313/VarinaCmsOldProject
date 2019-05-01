using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PersianDate;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.AutoMapperProfiles.Reslovers
{
    public class DateTimeConvertor : Profile
    {
        public DateTimeConvertor()
        {
            CreateMap<DateTime, string>().ConvertUsing(new DateTimeToPersianDateTimeConverter());
            CreateMap<string, DateTime>().ConvertUsing(new StringPersianDateTimeToDateTimeConverter());
            CreateMap<DateTime?, string>().ConvertUsing(new NullableDateTimeToStringPersianDateTimeConverter());
            CreateMap<string, DateTime?>().ConvertUsing(new NullableStringPersianDateTimeToDateTimeConverter());
            CreateMap<DateTime, LiquidDateTime>().ConvertUsing(new LiquidDateConvertor());
            CreateMap<LiquidDateTime, DateTime>().ConvertUsing(new NoNeedToConvertLiquidDate());
        }

     
    }

    public class NullableDateTimeToStringPersianDateTimeConverter : ITypeConverter<DateTime?, string>
    {
        public string Convert(DateTime? source, string destination, ResolutionContext context)
        {
            return source.HasValue? ConvertDate.ToFa(source, "yyyy/MM/dd - hh:mm"):"";
        }
    }


    internal class NullableStringPersianDateTimeToDateTimeConverter : ITypeConverter<string, DateTime?>
    {
        public DateTime? Convert(string source, DateTime? destination, ResolutionContext context)
        {
            return string.IsNullOrEmpty(source)?default(DateTime?): DateHelper.ParseSafe(source);
        }
    }

    public class NoNeedToConvertLiquidDate : ITypeConverter<LiquidDateTime, DateTime>
    {
        public DateTime Convert(LiquidDateTime source, DateTime destination, ResolutionContext context)
        {
            return DateTime.Now ;
        }
    }

    public class StringPersianDateTimeToDateTimeConverter : ITypeConverter<string, DateTime>
    {
        public StringPersianDateTimeToDateTimeConverter()
        {
        }

        public DateTime Convert(string source, DateTime destination, ResolutionContext context)
        {
            return DateHelper.ParseSafe(source);
        }
    }

    public class DateTimeToPersianDateTimeConverter : ITypeConverter<DateTime, string>
    {
        public string Convert(DateTime source, string destination, ResolutionContext context)
        {
            return ConvertDate.ToFa(source, "yyyy/MM/dd - hh:mm");
        }
    }
    public class LiquidDateConvertor : ITypeConverter<DateTime, LiquidDateTime>
    {
        public LiquidDateConvertor()
        {
        }

        public LiquidDateTime Convert(DateTime source, LiquidDateTime destination, ResolutionContext context)
        {
            return new LiquidDateTime(source);
        }
    }
}
