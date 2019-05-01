using System;
using System.IO;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Eshop;
using VarinaCmsV2.ViewModel.FileData;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class FileDataProfile : Profile
    {
        public FileDataProfile()
        {
            CreateMap<FileData, FileDataViewModelWithMeta>()
                .ForMember(x=>x.MetaData,opt=>opt.Ignore());
            CreateMap<FileDataAddOrUpdateVeiwModel, FileData>();
            CreateMap<FileDataViewModelWithMeta, FileData>();
            CreateMap<FileData, DownLoadFileLiquidAdapter>();

        }
    }
}