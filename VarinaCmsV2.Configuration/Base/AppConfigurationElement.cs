using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Configuration.Base
{
    public class AppConfigurationElement : System.Configuration.ConfigurationElement, IAppConfigurationElement
    {
        [ConfigurationProperty("name",IsRequired = true,IsKey = true)]
        public string Name
        {
            get { return base["name"] as string; }
            set { base["name"] = value; }
        }
        [ConfigurationProperty("value")]
        public string Value
        {
            get { return base["value"] as string; }
            set
            {
                base["value"] = value;
            }
        }

        public override bool IsReadOnly()
        {
            return false;
        }

    }
}
