using AutoMapper;
using Newtonsoft.Json.Linq;

using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class FieldProfile:Profile
    {
        public FieldProfile()
        {
            CreateMap<Field, FieldViewModel>().ForMember(x=>x.Fd,opt=>opt.MapFrom(s=>s.FieldDefenition));
            CreateMap<FieldViewModel, Field>();
            CreateMap<FieldAddOrUpdateViewModel, Field>();
            CreateMap<Field, FieldAddOrUpdateViewModel>();
        }
    }
}
