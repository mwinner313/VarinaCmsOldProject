using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.DomainClasses.Helpers
{
    public static class  AccessRestrictedItemHelper
    {
        public static ObservableCollection<AllowedRole> GetAllowedRoles(this IAccessRestrictedItem item)
        {
            var additionalPermissions =
                      JsonConvert.DeserializeObject<ObservableCollection<AllowedRole>>(item.AllowedRolesString ?? string.Empty)
                      ?? new ObservableCollection<AllowedRole>();
            additionalPermissions.CollectionChanged += item.SaveToPremissinBackingField;
            return additionalPermissions;
        }
        private static void SaveToPremissinBackingField(this IAccessRestrictedItem item, object sender, NotifyCollectionChangedEventArgs e)
        {
            var premissions = sender as ObservableCollection<AllowedRole>;
            if (premissions == null) throw new InvalidOperationException("Cant Observe Entitiy AllowedPremissions For DataPresistence");
            item.AllowedRoles = premissions;
        }
    }
}
