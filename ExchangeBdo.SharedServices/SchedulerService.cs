using Microsoft.Extensions.Logging;

namespace ExchangeBdo.SharedServices
{
    public interface ISchedulerService
    {
        void StartTimer(System.Timers.ElapsedEventHandler handler, double interval);
    }

    public class SchedulerService : ISchedulerService
    {
        private System.Timers.Timer? _timer;
        private readonly ILogger<SchedulerService> _logger;

        public SchedulerService(ILogger<SchedulerService> logger)
        {
            _logger = logger;
        }

        public void StartTimer(System.Timers.ElapsedEventHandler handler, double interval)
        {
            _timer = new System.Timers.Timer()
            {
                Interval = interval * 60 * 1000,
                Enabled = true,
                AutoReset = true
            };
            _timer.Elapsed += handler;

            _logger.LogInformation("{Service} is running.", nameof(SchedulerService));

            while (true)
                Thread.Sleep(1);
        }  
    }
}
