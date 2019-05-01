using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Forms;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class FormSchemeProfile:Profile
    {
        public FormSchemeProfile()
        {
            CreateMap<FormScheme, FormSchemeViewModel>();
            CreateMap<FormSchemeAddOrUpdateViewModel, FormScheme>();
        }
    }

}
