using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.Users
{
    public class Role : IdentityRole<Guid, UserRole>
    {
        public bool IsSystematic { get; set; }
        public string Premissions { get; set; }
        //public Portal Portal { get; set; }
        //public Guid PortalId { get; set; }
        [NotMapped]
        public ObservableCollection<Premission> PermissionsJObject
        {
            get
            {
                var additionalPermissions =
                    string.IsNullOrEmpty(Premissions) ? new ObservableCollection<Premission>() : JsonConvert.DeserializeObject<ObservableCollection<Premission>>(Premissions);
                additionalPermissions.CollectionChanged += SaveToPremissinBackingField;
                return additionalPermissions;
            }
            set => Premissions = JsonConvert.SerializeObject(value);
        }
        private void SaveToPremissinBackingField(object sender, NotifyCollectionChangedEventArgs e)
        {
            var premissions = sender as ObservableCollection<Premission>;
            this.PermissionsJObject = premissions ?? throw new InvalidOperationException("Cant Observe Entitiy AllowedPremissions For DataPresistence");
        }
    }
}
