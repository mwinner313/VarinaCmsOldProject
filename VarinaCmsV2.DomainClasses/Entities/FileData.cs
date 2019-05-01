using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Helpers;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class FileData :BaseEntity, IOptionalTrackibleItem, IAccessRestrictedItem, IHaveMetaData
    {
        public const string MetaTypeName = nameof(FileData);
        /// <summary>
        /// virtual path to file starts with / ex: /Uploads/test.png
        /// </summary>
        public string Path { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public User Creator { get; set; }
        public long Size { get; set; }
        public Guid? CreatorId { get; set; }
        public string AllowedRolesString { get; set; }
        public AccessType AccessType { get; set; }
        [NotMapped]
        public ObservableCollection<AllowedRole> AllowedRoles
        {
            get => this.GetAllowedRoles();
            set => AllowedRolesString = JsonConvert.SerializeObject(value);
        }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
    }
}
