using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Logic.Templating
{
    public class CacheTemplateProvider : ITemplateProvider
    {
        public string TemplatesBasePath { get; set; }
        public string Extension { get; set; }
        private readonly ITemplateFinderFactory _finderFactory;
        private readonly Dictionary<string, string> _cachedTemplates = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _templatesHash = new Dictionary<string, string>();
        public CacheTemplateProvider(ITemplateFinderFactory finderFactory)
        {
            _finderFactory = finderFactory;
        }
        public string Get404NotFoundTemplateString()
        {
            var finderStrategy = _finderFactory.Get404NotFoundStrategy(TemplatesBasePath);
            var templatePath = TemplatesBasePath + finderStrategy.GetTemplatePathDefault() + Extension;
            return GetTemplateString(templatePath);
        }
        public string GetHomePageTemplateString()
        {
            var finderStrategy = _finderFactory.GetHomePageStrategy(TemplatesBasePath);
            var templatePath = TemplatesBasePath + finderStrategy.GetTemplatePathDefault() + Extension;
            return GetTemplateString(templatePath);
        }
        public string GetTemplateString(ITemplatedItem item)
        {
            var finderStrategy = _finderFactory.GetStrategy(item);
            var templatePath = TemplatesBasePath + finderStrategy.GetTemplatePathByConvention(item) + Extension;
            if (!File.Exists(templatePath))
            {
                templatePath = TemplatesBasePath + finderStrategy.GetTemplatePathDefault(item) + Extension;
            }
            if (string.IsNullOrEmpty(templatePath)) throw new InvalidOperationException($"liquid viewnot found for {item.GetType()} handle => {item.Handle}");
            return GetTemplateString(templatePath);
        }

        private string GetTemplateString(string templatePath)
        {
            if (!_cachedTemplates.ContainsKey(templatePath))
                CacheTemplate(templatePath);
            if (IsTemplateTouchedFromLastTimeCache(templatePath))
                RefreshTemplateInCache(templatePath);
            return _cachedTemplates[templatePath];
        }

        private void RefreshTemplateInCache(string templatePath)
        {
            using (StreamReader reader = new StreamReader(templatePath))
            {
                _cachedTemplates[templatePath] = reader.ReadToEnd();
            }
            SaveHashForWachingFileLater(templatePath);
        }

        private void CacheTemplate(string templatePath)
        {
            CheckFileExists(templatePath);
            using (StreamReader reader = new StreamReader(templatePath))
            {
                _cachedTemplates.Add(templatePath, reader.ReadToEnd());
            }
            SaveHashForWachingFileLater(templatePath);
        }

        private void SaveHashForWachingFileLater(string templatePath)
        {
            var hash = CreateHash(templatePath);
            if (_templatesHash.ContainsKey(templatePath))
            {
                _templatesHash[templatePath] = hash;
            }
            else
            {
                _templatesHash.Add(templatePath, hash);
            }

        }

        private string CreateHash(string templatePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(templatePath))
                {
                    return ToHex(md5.ComputeHash(stream), true);
                }
            }
        }

        string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }

        private void CheckFileExists(string templatePath)
        {
            if (!File.Exists(templatePath)) throw new FileNotFoundException(templatePath);
        }

        private bool IsTemplateTouchedFromLastTimeCache(string templatePath)
        {
            return _templatesHash[templatePath] != CreateHash(templatePath);
        }


    }
}
