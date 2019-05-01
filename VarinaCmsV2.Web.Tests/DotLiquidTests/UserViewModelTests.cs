using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.ViewModel.User.Account;

namespace VarinaCmsV2.Web.Tests.DotLiquidTests
{
    [TestClass]
    public class UserViewModelTests
    {
        [TestMethod]
        public void ShouldRenderUserLiquidViewModel()
        {
            var str = Template.Parse("{{avatarPath}}").Render(Hash.FromAnonymousObject(new UserLiquidViewModel() { AvatarPath = "ali" }));
            Console.WriteLine(str);
        }
    }
}
