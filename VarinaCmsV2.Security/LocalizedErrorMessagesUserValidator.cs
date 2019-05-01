using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Security
{
    internal class LocalizedErrorMessagesUserValidator : UserValidator<User,Guid>
    {
        public LocalizedErrorMessagesUserValidator(UserManager<User, Guid> manager) : base(manager)
        {
        }

        public override Task<IdentityResult> ValidateAsync(User item)
        {
            return base.ValidateAsync(item);
        }
    }
}