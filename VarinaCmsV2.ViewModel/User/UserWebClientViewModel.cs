using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.ViewModel.User
{
    public class UserWebClientViewModel:BaseVeiwModel, IUrlableViewModel
    {
        public string Handle { get; set; }
        public string Title { get; set; }
        public string AvatarPath { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string ToUrl { get; set; }
        public string ToFullUrl { get; set; }
    }
}
