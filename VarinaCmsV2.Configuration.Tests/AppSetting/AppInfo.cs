using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Configuration.Base;
using VarinaCmsV2.Core.Contracts.Configuration.Base;

namespace VarinaCmsV2.Configuration.Tests.AppSetting
{
    [TestClass]
    public class AppInfoTests
    {
        [TestMethod]
        public void ShouldReadAppConfigFile()
        {
           
        }

        [TestMethod]
        public void ShouldChangeAppConfigFile()
        {
            var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).GetSection("varinaCms") as IVarinaConfigurationSection;
            cfg.AppInfo.Set("description", "golabi");
            cfg.SaveChanges();
            ConfigurationManager.RefreshSection("varinaCms");
           Assert.AreEqual(cfg.AppInfo.Get("description"),"golabi");
        }

    }
}
