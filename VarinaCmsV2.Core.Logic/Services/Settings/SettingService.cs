using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Services.Settings;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Logic.Services.Settings
{

    public class SettingService : ISettingService
    {
        private readonly ISettingDataService _settingDataService;
        private readonly IIdentityManager _identityManager;
        public SettingService(ISettingDataService settingDataService, IIdentityManager identityManager)
        {
            _settingDataService = settingDataService;
            _identityManager = identityManager;
        }

        public T GetSetting<T>() where T : class
        {
            return _settingDataService.Query.FirstOrDefault(x => x.Name == typeof(T).Name)?.Data.ToObject<T>();
        }

        public JObject GetSetting(string name)
        {
          var setting= _settingDataService.Query.FirstOrDefault(x => x.Name == name);
            if (setting is null) return null;
            return (_identityManager.IsCurrentIdentitySupervisor() || _identityManager.GetCurrentIdentityRoles().Any(x => setting.RolesToAccess.Split(',').Contains(x))) ? setting.Data
                : null;
        }

        public async Task SaveSetting(Setting model)
        {
            var setting = _settingDataService.Query.FirstOrDefault(x => x.Id == model.Id);
            if (setting is null) return;
            setting.Data = model.Data;
            if(_identityManager.IsCurrentIdentitySupervisor() || _identityManager.GetCurrentIdentityRoles().Any(x => setting.RolesToAccess.Split(',').Contains(x)))
                await _settingDataService.UpdateAsync(setting);
        }

        public List<Setting> GetAllSettings()
        {
            var settings= _settingDataService.Query.ToList();
            if (_identityManager.IsCurrentIdentitySupervisor()) return settings;
            var allowedSettings=new List<Setting>();
            foreach (var setting in settings)
            {
                if(  _identityManager.GetCurrentIdentityRoles().Any(x=> setting.RolesToAccess.Split(',').Contains(x)))
                    allowedSettings.Add(setting);
            }
            return allowedSettings;
        }
    }
}
