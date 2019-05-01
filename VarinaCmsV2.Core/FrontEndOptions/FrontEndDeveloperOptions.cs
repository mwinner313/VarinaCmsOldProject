using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace VarinaCmsV2.Core.FrontEndOptions
{
    public class FrontEndDeveloperOptions
    {
        private static FrontEndDeveloperOptions _instance = GetInitialized();

        private static FrontEndDeveloperOptions GetInitialized()
        {
            string optionFilePath = SettingHelper.WebSiteThemePath + "/front-developer-options.json";
            string json =File.ReadAllText(optionFilePath);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return ser.Deserialize<FrontEndDeveloperOptions>(json);
        }


        public FrontEndDeveloperOptions()
        {
        }

        public static FrontEndDeveloperOptions Instance => GetInitialized();

        public ImageOptions Images { get; set; }
        public PaginationOption Pagination { get; set; }
        public int MegaMenuItemSize { get; set; }
        public List<RelatedEntityLoadOption> EntityRelatedItemsLoadOptions { get; set; }
    }
}
