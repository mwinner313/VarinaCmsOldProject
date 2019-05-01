using VarinaCmsV2.Services.ScheduledJobs;
using Registry = FluentScheduler.Registry;

namespace VarinaCmsV2.Core.Logic
{
    public class JobsRegistry: Registry
    {
        public JobsRegistry()
        {
            Schedule<ProductsExpiredReservations>().ToRunEvery(1).Hours();
        }
    }
}
