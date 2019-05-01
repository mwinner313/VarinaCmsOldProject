using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Logic.DataSeeds
{
    public class UserManagerSeedDataBaseContext : IUserManagerSeedDataBaseContext
    {
        public List<User> InitialUsers { get; set; } = new List<User>()
        {
            new User()
            {
                UserName = "m.ghanbari01375@gmail.com",
                Email = "m.ghanbari01375@gmail.com",
                PasswordHash = "magh1375",
                PhoneNumber = "09196644336",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                Name = "mohammadali",
                CreateDateTime = DateTime.Now,
                UpdateDateTime =  DateTime.Now,
                Handle = "mohammadali",
                Url = "mohammadali"
            },
            new User()
            {
                UserName = "vahid.bagherian@gmail.com",
                Email = "vahid.bagherian@gmail.com",
                PasswordHash = "09132631880",
                PhoneNumber = "09132631880",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                Name = "vahid.bagherian",
                  Handle = "vahid",
                Url = "vahid",
                CreateDateTime = DateTime.Now,
                UpdateDateTime =  DateTime.Now,
            },
            new User()
            {
                UserName = "admin",
                PasswordHash = "varina@varina",
                Name = "مدیر وب سایت",
                CreateDateTime = DateTime.Now,
                UpdateDateTime =  DateTime.Now,
                   Handle = "admin",
                Url = "admin",
            },
        };

        public List<Role> InitialRoles { get; set; } = new List<Role>()
        {
            new Role()
            {
                Name = PreDefRoles.PrincipalAdministrator,
                IsSystematic = true,
                PermissionsJObject = new ObservableCollection<Premission>()
                {
                    new Premission() {Action = SchemePremission.CanSee, IsSystematic = true},
                    new Premission() {Action = PanelPremission.Default, IsSystematic = true},
                    new Premission() {Action = PagePremision.CanSee, IsSystematic = true},
                    new Premission() {Action = PagePremision.CanManage, IsSystematic = true},
                    new Premission() {Action = EntityPremission.CanSee, IsSystematic = true},
                    new Premission() {Action = EntityPremission.CanAdd, IsSystematic = true},
                    new Premission() {Action = EntityPremission.CanEdit, IsSystematic = true},
                    new Premission() {Action = EntityPremission.CanDelete, IsSystematic = true},
                    new Premission() {Action = CategoryPremission.CanSee, IsSystematic = true},
                    new Premission() {Action = CategoryPremission.CanAdd, IsSystematic = true},
                    new Premission() {Action = CategoryPremission.CanEdit, IsSystematic = true},
                    new Premission() {Action = CategoryPremission.CanDelete, IsSystematic = true},
                    new Premission() {Action = UserManagePremission.CanSeeUsers, IsSystematic = true},
                    new Premission() {Action = UserManagePremission.CanAdd, IsSystematic = true},
                    new Premission() {Action = UserManagePremission.CanDelete, IsSystematic = true},
                    new Premission() {Action = UserManagePremission.CanUpdate, IsSystematic = true},
                }
            },
            new Role()
            {
                Name = PreDefRoles.Developer,
                IsSystematic = true,
                PermissionsJObject = new ObservableCollection<Premission>()
                {
                    new Premission() {Action = SchemePremission.CanManage, IsSystematic = true},
                    new Premission() {Action = SchemePremission.CanSee, IsSystematic = true},
                    new Premission() {Action = PanelPremission.Default, IsSystematic = true},
                    new Premission() {Action = PagePremision.CanSee, IsSystematic = true},
                    new Premission() {Action = PagePremision.CanManage, IsSystematic = true},
                    new Premission() {Action = EntityPremission.CanSee, IsSystematic = true},
                    new Premission() {Action = EntityPremission.CanAdd, IsSystematic = true},
                    new Premission() {Action = EntityPremission.CanEdit, IsSystematic = true},
                    new Premission() {Action = EntityPremission.CanDelete, IsSystematic = true},
                    new Premission() {Action = CategoryPremission.CanSee, IsSystematic = true},
                    new Premission() {Action = CategoryPremission.CanAdd, IsSystematic = true},
                    new Premission() {Action = CategoryPremission.CanEdit, IsSystematic = true},
                    new Premission() {Action = CategoryPremission.CanDelete, IsSystematic = true},
                }
            },
            new Role()
            {
                Name = PreDefRoles.Supervisor,
                IsSystematic = true,
                PermissionsJObject = new ObservableCollection<Premission>()
                {
                    new Premission() {Action = SchemePremission.CanManage, IsSystematic = true},
                    new Premission() {Action = SchemePremission.CanSee, IsSystematic = true},
                    new Premission() {Action = PanelPremission.Default, IsSystematic = true},
                    new Premission() {Action = PagePremision.CanSee, IsSystematic = true},
                    new Premission() {Action = PagePremision.CanManage, IsSystematic = true},
                    new Premission() {Action = EntityPremission.CanSee, IsSystematic = true},
                    new Premission() {Action = EntityPremission.CanAdd, IsSystematic = true},
                    new Premission() {Action = EntityPremission.CanEdit, IsSystematic = true},
                    new Premission() {Action = EntityPremission.CanDelete, IsSystematic = true},
                    new Premission() {Action = CategoryPremission.CanSee, IsSystematic = true},
                    new Premission() {Action = CategoryPremission.CanAdd, IsSystematic = true},
                    new Premission() {Action = CategoryPremission.CanEdit, IsSystematic = true},
                    new Premission() {Action = CategoryPremission.CanDelete, IsSystematic = true},
                    new Premission() {Action = UserManagePremission.CanSeeUsers, IsSystematic = true},
                    new Premission() {Action = UserManagePremission.CanAdd, IsSystematic = true},
                    new Premission() {Action = UserManagePremission.CanDelete, IsSystematic = true},
                    new Premission() {Action = UserManagePremission.CanUpdate, IsSystematic = true},
                }
            },
            new Role()
            {
                Name = PreDefRoles.Author,
                IsSystematic = true,
                PermissionsJObject = new ObservableCollection<Premission>()
                {
                    new Premission() {Action = SchemePremission.CanSee, IsSystematic = true},
                    new Premission() {Action = PanelPremission.Default, IsSystematic = true},
                    new Premission() {Action = PagePremision.CanSee, IsSystematic = true},
                    new Premission() {Action = PagePremision.CanManage, IsSystematic = true},
                    new Premission() {Action = EntityPremission.CanSee, IsSystematic = true},
                    new Premission() {Action = EntityPremission.CanAdd, IsSystematic = true},
                    new Premission() {Action = EntityPremission.CanEdit, IsSystematic = true},
                    new Premission() {Action = EntityPremission.CanDelete, IsSystematic = true},
                    new Premission() {Action = CategoryPremission.CanSee, IsSystematic = true},
                    new Premission() {Action = CategoryPremission.CanAdd, IsSystematic = true},
                    new Premission() {Action = CategoryPremission.CanEdit, IsSystematic = true},
                    new Premission() {Action = CategoryPremission.CanDelete, IsSystematic = true},
                }
            }
            ,
            new Role()
            {
                Name = PreDefRoles.WebSiteUser,
                IsSystematic = true,
            },
            new Role()
            {
                Name = PreDefRoles.Guest,
                IsSystematic = true
            }
        };

        public async Task PostDataInitializeAsync(IAppUserManager userManager)
        {
            var ghanbari = await userManager.FindByNameAsync("m.ghanbari01375@gmail.com");
            var bagherian = await userManager.FindByNameAsync("vahid.bagherian@gmail.com");
            var admin = await userManager.FindByNameAsync("admin");

            await userManager.AddToRoleAsync(ghanbari.Id, PreDefRoles.Developer);
            await userManager.AddToRoleAsync(ghanbari.Id, PreDefRoles.Supervisor);
            await userManager.AddToRoleAsync(bagherian.Id, PreDefRoles.Developer);
            await userManager.AddToRoleAsync(bagherian.Id, PreDefRoles.Supervisor);
            await userManager.AddToRoleAsync(admin.Id, PreDefRoles.PrincipalAdministrator);
        }
    }
}