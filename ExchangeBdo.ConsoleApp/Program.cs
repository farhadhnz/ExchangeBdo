using ExchangeBdo.SharedDtos;
using ExchangeBdo.SharedServices;
using ExchangeBdo.ConsoleApp.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ExchangeBdo.Data.Repositories;
using ExchangeBdo.Data;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateDefaultBuilder(args);

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

builder.ConfigureServices(services =>
{
    services.AddTransient<IRateCalculationService, RateCalculationService>();
    services.AddTransient<IInputValidationService, InputValidationService>();
    services.AddTransient<IOnlineDataProvider, OnlineDataProvider>();
    services.AddTransient<IInputOutputProvider, InputOutputProvider>();
    services.AddTransient<ISymbolRepository, SymbolRepository>();
    services.AddTransient<IExchangeRepository, ExchangeRepository>();
    services.AddLogging();
    services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(config.GetConnectionString("ExchangeConnectionString")));
    services.AddLogging();
});

var app = builder.Build();

var inputOutputProvider = app.Services.GetRequiredService<IInputOutputProvider>();

while (true)
{
    await inputOutputProvider.ProcessInputOutput();
}




