using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;
using VarinaCmsV2.Core.Logic.Security;

namespace VarinaCmsV2.Core.Logic.Tests.Security
{
    [TestClass]
    public class SecurityLoggerTests
    {
        [TestMethod]
        public void ShouldLogSecurityAttempsToTelegram()
        {
            var logger=new SecurityLogger();
            logger.LogDangeriousUpdateAttemp(Helper.GetMockPrincipal(),new HtmlFieldTemplate() {Content = "sdada"});
        }

    }

}
