using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using ExchangeBdo.Data;
using ExchangeBdo.Data.Repositories;
using ExchangeBdo.SharedServices;
using ExchangeBdo.SharedDtos;
using ExchangeBdo.SchedulingApp;

var builder = Host.CreateDefaultBuilder(args);

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

builder.ConfigureServices(services =>
{
    services.AddSingleton<ISchedulerService, SchedulerService>();

    services.AddHttpClient<IApiFetchingService<ExchangeDto>, ApiFetchingService<ExchangeDto>>
    ("FetchService", hc => ConfigureHttpClient(hc));
    services.AddHttpClient<IApiFetchingService<SymbolDto>, ApiFetchingService<SymbolDto>>
    ("FetchService", hc => ConfigureHttpClient(hc));

    services.AddTransient<IDataFeederService, DataFeederService>();
    services.AddTransient<ISymbolRepository, SymbolRepository>();
    services.AddTransient<IExchangeRepository, ExchangeRepository>();
    services.AddTransient<IExchangeService, ExchangeService>();
    services.AddTransient<IExchangeSchedulerService, ExchangeSchedulerService>();
    services.AddLogging();
    services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(config.GetConnectionString("ExchangeConnectionString")));
});

void ConfigureHttpClient(HttpClient hc)
{
    hc.DefaultRequestHeaders.Clear();
    hc.DefaultRequestHeaders.Accept.Add(
        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    hc.DefaultRequestHeaders.Add("apikey", config.GetValue<string>("FixerApiKey"));
    hc.BaseAddress = new Uri("https://api.apilayer.com/fixer/");
}

var app = builder.Build();
await app.StartAsync();

var schedulerService = app.Services.GetRequiredService<IExchangeSchedulerService>();
schedulerService.SetBaseCurrency(config.GetValue<string>("BaseCurrency"));
await schedulerService.ProcessScheduling(double.Parse(config.GetValue<string>("Scheduling Interval")));

