using System;
using System.Web.Configuration;

namespace VarinaCmsV2.Core
{
    public static class SettingHelper
    {
        public static bool MultiLanguageEnabled => WebConfigurationManager.AppSettings.Get("multi-language-mode") == "true";
        public static string UploadsBasePath => WebConfigurationManager.AppSettings.Get("site-upload-path");
        public static string UsersAvatarsPath => UploadsBasePath + "Users/";
        public static string WebSiteThemePath => AppDomain.CurrentDomain.BaseDirectory + WebConfigurationManager.AppSettings.Get("template-base-path");
    }

  
}
