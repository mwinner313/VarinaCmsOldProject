using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using VarinaCmsV2.Common;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Security
{
    public class EmailSecuriyService:IEmailSecuriyService
    {
        private readonly EmailInfo _senderInfo;
        public EmailSecuriyService(EmailInfo sender)
        {
            _senderInfo = sender;
        }

        public async Task SendAsync(IdentityMessage message)
        {
            var mail=new MailMessage();
            mail.To.Add(new MailAddress(message.Destination));
            mail.From = new MailAddress(_senderInfo.From);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _senderInfo.UserName,  
                    Password = _senderInfo.Password
                };
                smtp.Credentials = credential;
                smtp.Host = _senderInfo.Host;
                smtp.Port = _senderInfo.Port;
                smtp.EnableSsl = _senderInfo.IsSSlEnabled;
                await smtp.SendMailAsync(mail);
            }

        }
    }
}
