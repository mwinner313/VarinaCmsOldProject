using System.Security.Principal;
using System.Text;
using System.Web;
using Microsoft.AspNet.Identity;
using NetTelebot;
using Newtonsoft.Json;
using VarinaCmsV2.Common.MvcFilters;
using VarinaCmsV2.Core.Contracts.Security;

namespace VarinaCmsV2.Core.Logic.Security
{
    //TODO test
    //TODO impelement telegram messaging email
    public class SecurityLogger: ISecurityLogger
    {
        private readonly TelegramBotClient _botClient;
        public SecurityLogger()
        {
            _botClient =  new TelegramBotClient() { Token = TelegramErrorLoggerFilterAttribute.TelegramBotToken };
        }
        public void LogDangeriousUpdateAttemp(IPrincipal principal, object updateTarget)
        {
            SendMessage(principal, updateTarget,"Update");
        }
        public void LogDangeriousDeleteAttemp(IPrincipal principal, object deleteTarget)
        {
            SendMessage(principal, deleteTarget, "Delete");
        }

        public void LogDangeriousAddAttemp(IPrincipal principal, object data)
        {
            SendMessage(principal, data, "Add");
        }

        public void LogDangeriousReadAttemp(IPrincipal principal, object data)
        {
            SendMessage(principal, data, "Read");
        }

        public void LogDangeriousActionAttemp(IPrincipal principal, object data, string action)
        {
            SendMessage(principal, data, action);
        }

        private void SendMessage(IPrincipal principal, object target, string action)
        {
            var message = new StringBuilder($"user : {principal.Identity.GetUserName()}");
            message.AppendLine("سایت ::");
            message.AppendLine(HttpContext.Current.Request.Url.Host);
            message.AppendLine($"action : {action}");
            message.AppendLine("data : ");
            message.AppendLine(JsonConvert.SerializeObject(target));
            message.AppendLine("identity : ");
            message.AppendLine(JsonConvert.SerializeObject(target));
            _botClient.SendMessage(TelegramErrorLoggerFilterAttribute.TelegramChatId, message.ToString());
        }
    }
}
