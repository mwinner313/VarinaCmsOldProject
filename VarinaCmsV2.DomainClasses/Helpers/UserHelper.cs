using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.DomainClasses.Helpers
{
    public static class UserHelper
    {
        public static ObservableCollection<Premission> GetAdditionalPremissions(this User user)
        {
            var additionalPermissions
                 = string.IsNullOrEmpty(user.AdditionalPermissionsJobject) ? new ObservableCollection<Premission>() : JsonConvert.DeserializeObject<ObservableCollection<Premission>>(user.AdditionalPermissionsJobject);
            additionalPermissions.CollectionChanged += user.SaveToPremissinBackingField;
            return additionalPermissions;
        }
        private static void SaveToPremissinBackingField(this User user, object sender, NotifyCollectionChangedEventArgs e)
        {
            var premissions = sender as ObservableCollection<Premission>;
            if (premissions == null) throw new InvalidOperationException("Cant Observe User Premissions For DataPresistence");
            user.AdditionalPermissions = premissions;
        }
    }
}
