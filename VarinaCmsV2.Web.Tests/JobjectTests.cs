using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace VarinaCmsV2.Web.Tests
{
    [TestClass]
    public class JsonTests
    {
        internal class Boy
        {
            public string Name { get; set; }
        }
        [TestMethod]
        public void ShouldSerializeDataAsString()
        {
            var boyJobject = JsonConvert.SerializeObject(new Boy() { Name = "ali" }, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            Console.Write(boyJobject);
        }
        [TestMethod]
        public void ShouldSerializeAndDeserializeDataAsJobject()
        {
            var boyJobject = JObject.Parse(JsonConvert.SerializeObject(new Boy() { Name = "ali" }));

            var boy = JsonConvert.DeserializeObject<Boy>(boyJobject.ToString());
            Assert.AreEqual(boy.Name, "ali");
        }
    }
}
