using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Services.File;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class FileDataQueryHelper
    {
        public static IQueryable<FileData> Filter(this IQueryable<FileData> fileDatas, FileQuery query)
        {
            fileDatas = FilterBySearchText(fileDatas, query);
            fileDatas = FilterByExtension(fileDatas, query);

            return fileDatas;
        }

        private static IQueryable<FileData> FilterByExtension(IQueryable<FileData> fileDatas, FileQuery query)
        {
            return string.IsNullOrEmpty(query.Extension)
                ? fileDatas
                : fileDatas.Where(x => query.Extension.Contains(x.Extension));
        }

        private static IQueryable<FileData> FilterBySearchText(IQueryable<FileData> fileDatas, FileQuery query)
        {
            return string.IsNullOrEmpty(query.SearchText)
                ? fileDatas
                : fileDatas.Where(x => x.Name.Contains(query.SearchText));
        }
    }
}
