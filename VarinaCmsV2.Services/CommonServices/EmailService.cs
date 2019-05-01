
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;

namespace VarinaCmsV2.Services.CommonServices
{
    public class EmailService : IEmailyService
    {
        private readonly IMessageServiceAccountDataService _messageServiceAccountDataService;
        public EmailService(IMessageServiceAccountDataService messageServiceAccountDataService)
        {
            _messageServiceAccountDataService = messageServiceAccountDataService;
        }

        public async Task SendAsync(EmailContext context)
        {
            var senderInfo =
                GetSenderInfo();
            var mail = new MailMessage();
            mail.To.Add(new MailAddress(context.To));
            mail.From = new MailAddress(senderInfo.From);
            mail.Subject = context.Subject;
            mail.Body = context.Content;
            mail.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = senderInfo.UserName,
                    Password = senderInfo.Password
                };
                smtp.Credentials = credential;
                smtp.Host = senderInfo.Host;
                smtp.Port = senderInfo.Port;
                smtp.EnableSsl = senderInfo.IsSSlEnabled;
                await smtp.SendMailAsync(mail);
            }
        }

        private EmailInfo GetSenderInfo()
        {
            var account = _messageServiceAccountDataService.Query.FirstOrDefault(x =>
                x.Type.ToLower() == "email" && x.IsDefaultForUse);

            if (account is null)
            {
             TelegramMessenger.SendMessage($"ارسال کننده پیش فرض ایمیل یافت نشد {HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}");
                throw new InvalidOperationException($"ارسال کننده پیش فرض ایمیل یافت نشد {HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}");
            }
            return account.MetaData.ToObject<EmailInfo>();
        }
    }
}
