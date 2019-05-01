using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Web.Infrastructure
{
    public class SecureAngularJsMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public const string AngularProtectionJsonPrefix = ")]}',\n";
        public SecureAngularJsMediaTypeFormatter()
        {
            this.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }
        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,
            TransportContext transportContext)
        {
            PrependAngularProtectionStringAsync(writeStream);
            return base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
        }

        private Task PrependAngularProtectionStringAsync(Stream stream)
        {
            stream.WriteAsync(Encoding.UTF8.GetBytes(AngularProtectionJsonPrefix), 0, AngularProtectionJsonPrefix.Length);
            return Task.FromResult(0);
        }

        private void PrependAngularProtectionString(Stream stream)
        {
            AsyncHelper.RunSync(() => PrependAngularProtectionStringAsync(stream));
        }
    }
}