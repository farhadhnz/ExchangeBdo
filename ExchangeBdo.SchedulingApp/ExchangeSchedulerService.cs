using ExchangeBdo.SharedServices;
using Microsoft.Extensions.Logging;
using System.Timers;

namespace ExchangeBdo.SchedulingApp
{

    public class ExchangeSchedulerService : IExchangeSchedulerService
    {
        private readonly IDataFeederService _dataFeederService;
        private readonly IExchangeService _exchangeService;
        private readonly ILogger<ExchangeSchedulerService> _logger;
        private readonly ISchedulerService _schedulerService;

        private string BaseCurrency = "USD";

        public ExchangeSchedulerService(IDataFeederService dataFeederService,
            IExchangeService exchangeService,
            ILogger<ExchangeSchedulerService> logger,
            ISchedulerService schedulerService)
        {
            _dataFeederService = dataFeederService;
            _exchangeService = exchangeService;
            _logger = logger;
            _schedulerService = schedulerService;
        }

        public async Task ProcessScheduling(double interval)
        {
            // Get Symbols Data from API and save to db if not exists
            await _dataFeederService.FeedSymbols();

            var elapsed = new ElapsedEventHandler(schedulingEvent);

            _schedulerService.StartTimer(elapsed, interval);
        }

        public void SetBaseCurrency(string currency)
        {
            BaseCurrency = currency;
        }

        private async void schedulingEvent(object source, ElapsedEventArgs e)
        {
            _logger.LogInformation("Scheduling Event Triggered...");
            await _exchangeService.SaveExchangeData(BaseCurrency);
        }
    }

    public interface IExchangeSchedulerService
    {
        Task ProcessScheduling(double interval);
        void SetBaseCurrency(string currency);
    }
}
