using System;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using NetTelebot;

namespace VarinaCmsV2.Common.MvcFilters
{
    //https://api.telegram.org/bot343855714:AAGDNb_XNB1nw3BFkEt4GNdvu3rERJJLyoo/sendMessage?chat_id=78654448&text=hello
    public class TelegramErrorLoggerFilterAttribute : Attribute, IExceptionFilter
    {
        public const int TelegramChatId = 78654448;
        public const string TelegramBotToken = "343855714:AAGDNb_XNB1nw3BFkEt4GNdvu3rERJJLyoo";
        public TelegramErrorLoggerFilterAttribute()
        {
          
        }
        public void OnException(ExceptionContext filterContext)
        {
            SendErrorDetailsToTelgramBot(filterContext.Exception);
        }

        private void SendErrorDetailsToTelgramBot(Exception ex)
        {
            if (WebConfigurationManager.AppSettings.Get("telegram-error-logs") == "true")
            {
                string message = CreateMessageWithDetails(ex.Message);
                var botClient = new TelegramBotClient() { Token = TelegramBotToken };
                botClient.SendMessage(TelegramChatId, message);
            }
        }

        private string CreateMessageWithDetails(string err)
        {
            var whichSiteExHappend = HttpContext.Current.Request.Url.Host;
            var messageString = new StringBuilder(whichSiteExHappend);
            messageString.AppendLine("سایت ::");
            messageString.AppendLine(HttpContext.Current.Request.Url.Host);
            messageString.AppendLine("خطا ::");
            messageString.AppendLine(err);
            messageString.AppendLine("کاربر :: ");
            messageString.AppendLine(Thread.CurrentPrincipal.Identity.Name);
            messageString.AppendLine();
            return messageString.ToString();
        }
    }
}
