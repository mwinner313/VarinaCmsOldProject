using Owin;
using StructureMap;

namespace VarinaCmsV2.Web.StartUpChainOfResoinsibility
{
    public interface IStartupHandler
    {
        IStartupHandler Next { get; set; }
        void Process(IAppBuilder app , IContainer container);
    }
}