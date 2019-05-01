
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.FileData;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class FileDataMapHelper
    {
        public static List<FileDataViewModelWithMeta> MapListToViewModel(this IQueryable<FileData> fileDatas)
        {
            return Mapper.Map<List<FileDataViewModelWithMeta>>(fileDatas);
        }
        public static FileDataViewModelWithMeta MapToViewModel(this FileData fileData)
        {
            return Mapper.Map<FileDataViewModelWithMeta>(fileData);
        }
        public static FileDataViewModelWithMeta MapToViewModelWithMeta(this FileData fileData,List<Meta> metas)
        {
            var vm= Mapper.Map<FileDataViewModelWithMeta>(fileData);
            vm.MetaData = metas.MapToViewModelList();
            return vm;
        }
        public static async Task<List<FileDataViewModelWithMeta>> MapListToViewModelWithMeta(this IQueryable<FileData> fileDatas, IQueryable<Meta> metas)
        {

            var filesWithMeta = new List<FileDataViewModelWithMeta>();
            var data =
            await fileDatas.Select(
                        x =>
                            new
                            {
                                File = x,
                                x.Creator,
                                Metas = metas.Where(m => m.TargetId == x.Id && m.TargetType == FileData.MetaTypeName)
                            })
                    .ToListAsync();
            data.ForEach(x =>
            {
                var vm = x.File.MapToViewModel();
               
                vm.MetaData = x.Metas.MapToViewModelList();
                vm.CreatorName = x.Creator?.Name;
                vm.CreatorUserName = x.Creator?.UserName;
                filesWithMeta.Add(vm);
            });
            return filesWithMeta;
        }

        public static void MapToExisting(this FileDataAddOrUpdateVeiwModel meta,FileData existing)
        {
            Mapper.Map(meta, existing);
        }


    }
}
