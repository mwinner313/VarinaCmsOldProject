using VarinaCmsV2.Common;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.Data.DbContexts;
using VarinaCmsV2.Services.CommonServices;
using VarinaCmsV2.Services.DataServices;

namespace VarinaCmsV2.Services.ScheduledJobs
{
    public class UserMessageNotifier : InjectibleJob
    {
        private readonly IEmailyService _emailyService;
        private readonly string _email;
        private readonly string _subject;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _content;
        public UserMessageNotifier(string email, string subject, string content)
        {
            _email = email;
            _subject = subject;
            _content = content;
            _unitOfWork = new AppDbContext();
            _emailyService =new EmailService(new MessageServiceAccountDataService(_unitOfWork));
        }

        public override void Execute()
        {
            AsyncHelper.RunSync(() =>
                _emailyService.SendAsync(new EmailContext()
                {
                    To = _email,
                    Subject = _subject,
                    Content = _content
                }));
            _unitOfWork.Dispose();
        }
    }
}
