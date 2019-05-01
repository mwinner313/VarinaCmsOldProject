using System.Configuration;
using VarinaCmsV2.Core.Contracts.Configuration.Base;

namespace VarinaCmsV2.Configuration.Base
{
    public  class AppConfigurationElementCollection : System.Configuration.ConfigurationElementCollection, IAppConfigurationElementCollection
    {
       
        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new AppConfigurationElement();
        }

        protected override object GetElementKey(System.Configuration.ConfigurationElement element)
        {
           return ((AppConfigurationElement)element).Name;
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        public string Get(string name)
        {
            return (BaseGet(name) as AppConfigurationElement)?.Value;
        }

        public void Set(string name, string value)
        {
         (BaseGet(name) as AppConfigurationElement).Value=value;
        }
      

        public void Add(IAppConfigurationElement el)
        {
            BaseAdd(el as AppConfigurationElement);
        }

        public void Add(Core.Contracts.Configuration.Base.IAppConfigurationElement el)
        {
            throw new System.NotImplementedException();
        }
    }
}
