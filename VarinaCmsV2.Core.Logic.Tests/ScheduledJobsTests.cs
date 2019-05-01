using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Common;
using VarinaCmsV2.IocCofig;
using VarinaCmsV2.Services.ScheduledJobs;

namespace VarinaCmsV2.Core.Logic.Tests
{
    [TestClass]
    public class ScheduledJobsTests
    {
        [TestMethod]
        public void ShouldSendEmail()
        {
            InjectibleJob.Container = AppObjectFactory.GetContainer();
            new UserMessageNotifier("m.ghanbari01375@gmail.com","test","test").Execute();

        }
    }
}
