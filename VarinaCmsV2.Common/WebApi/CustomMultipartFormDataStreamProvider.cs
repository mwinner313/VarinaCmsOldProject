using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Common.WebApi
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        const string Illegals = "\"M\"\\a/ry/ h**ad:>> a\\/:*?\"| li*tt|le|| la\"mb.?";
        private readonly string _path;
        public CustomMultipartFormDataStreamProvider(string path) : base(path, 31457280)
        {
            _path = path;   
        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName";
            return MakeUniqeName(_path + name.Replace("\"", ""));
        }
        private string MakeUniqeName(string path)
        {
            int count = 0;
            var dir = Path.GetDirectoryName(path);
            var fileName = Path.GetFileNameWithoutExtension(path);
            var tempFileName = fileName;
            var ext = Path.GetExtension(path);
            while (File.Exists(path))
            {
                tempFileName = $"{fileName}({count++})";
                path = Path.Combine(dir, tempFileName + ext);
            }
            return tempFileName + ext;
        }

       
    }
}
