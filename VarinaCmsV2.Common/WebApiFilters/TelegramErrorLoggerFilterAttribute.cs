using System;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Http.Filters;
using System.Web.Mvc;
using NetTelebot;
using IExceptionFilter = System.Web.Http.Filters.IExceptionFilter;

namespace VarinaCmsV2.Common.WebApiFilters
{
    //https://api.telegram.org/bot343855714:AAGDNb_XNB1nw3BFkEt4GNdvu3rERJJLyoo/sendMessage?chat_id=78654448&text=hello
    public class TelegramErrorLoggerFilterAttribute : System.Web.Http.Filters.ExceptionFilterAttribute,IExceptionFilter
    {
    

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            SendErrorDetailsToTelgramBot(actionExecutedContext.Exception);
            base.OnException(actionExecutedContext);
        }
        private void SendErrorDetailsToTelgramBot(Exception ex)
        {
            if (WebConfigurationManager.AppSettings.Get("telegram-error-logs") == "true")
            {
                string message = CreateMessageWithDetails(ex);
                var botClient = new TelegramBotClient() {Token = "343855714:AAGDNb_XNB1nw3BFkEt4GNdvu3rERJJLyoo"};
                botClient.SendMessage(78654448, message);
            }
        }
        private string CreateMessageWithDetails(Exception ex)
        {
            var whichSiteExHappend = HttpContext.Current.Request.Url.Host;
            var messageString = new StringBuilder(whichSiteExHappend);
            messageString.AppendLine("سایت ::");
            messageString.AppendLine(HttpContext.Current.Request.Url.Host);
            messageString.AppendLine("خطا ::");
            AddErrors(messageString, ex);
            messageString.AppendLine("کاربر :: ");
            messageString.AppendLine(Thread.CurrentPrincipal.Identity.Name);
            messageString.AppendLine();
            return messageString.ToString();
        }

        private void AddErrors(StringBuilder messageString, Exception exception)
        {
            messageString.AppendLine(exception.Message);
            messageString.AppendLine("--------------------------------------------");
            if(exception.InnerException!=null) AddErrors(messageString,exception.InnerException);
        }
    }
}
