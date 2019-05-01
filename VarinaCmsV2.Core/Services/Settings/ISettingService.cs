using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Services.Settings
{
    public interface ISettingService
    {
        T GetSetting<T>() where T : class;
        JObject GetSetting(string name);
        Task SaveSetting(Setting model);
        List<Setting> GetAllSettings();
    }
}
