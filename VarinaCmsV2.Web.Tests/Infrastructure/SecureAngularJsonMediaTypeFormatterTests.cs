//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http;

//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace VarinaCmsV2.Web.Tests.Infrastructure
//{
//    [TestClass]
//    public class SecureAngularJsonMediaTypeFormatterTests
//    {
//       private readonly  TestController _testController=new TestController(new List<TestController.Product>());
//        public SecureAngularJsonMediaTypeFormatterTests()
//        {
            
//        }
//        [TestMethod]
//        public void ShouldAddSecureStringInResponseBody()
//        {
//            _testController.Request=new HttpRequestMessage();
//            _testController.Configuration = new HttpConfiguration();
//            _testController.Configuration.Formatters.Insert(0,new SecureAngularJsMediaTypeFormatter());
//            var response = _testController.GetAllProducts();
//            Assert.IsTrue(response.ExecuteAsync(new CancellationToken()).Result.Content.ReadAsStringAsync().Result.StartsWith(SecureAngularJsMediaTypeFormatter.AngularProtectionJsonPrefix));
//        }

        
//    }
//}
