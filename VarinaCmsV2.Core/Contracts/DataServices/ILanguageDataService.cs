using System;
using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Contracts.DataServices
{
    public interface ILanguageDataService: ICachedDataService<Language,string>
    {
        IEnumerable<Language> FromCache();

    }
}
