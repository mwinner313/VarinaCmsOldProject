using System.Web;
using NetTelebot;

namespace VarinaCmsV2.Common
{
    public static class TelegramMessenger
    {
        public static void SendMessage(string message)
        {
            var botClient = new TelegramBotClient() { Token = "343855714:AAGDNb_XNB1nw3BFkEt4GNdvu3rERJJLyoo" };
            botClient.SendMessage(78654448, HttpContext.Current?.Request.Url.Host + "/elmah.axd::\n" + message +"\n");
        }
    }
}
