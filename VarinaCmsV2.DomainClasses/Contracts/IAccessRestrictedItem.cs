using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public interface IAccessRestrictedItem
    {
        [NotMapped]
        ObservableCollection<AllowedRole> AllowedRoles { get; set; }
        string AllowedRolesString { get; set; }
        AccessType AccessType { get; set; }
    }

    public enum AccessType
    {

        Public = 0,
        Restricted = 10
    }

    public enum AccessPremission
    {
        See = 5,
        Manage = 10,
    }
    public class AllowedRole
    {
        public string RoleName { get; set; }
        public AccessPremission AccessPremission { get; set; }
    }
}
