// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Tgs.GameRecord.ManualConsole;
using Tgs.GameRecord.ManualConsole.Operators;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", true)
    .AddEnvironmentVariables();

builder.Services.AddTransient<IGameProvidersService, GameProvidersService>();
builder.Services.AddTransient<IConfigurationService, ConfigurationService>();
builder.Services.AddTransient<IDateTypesService, DateTypesService>();
builder.Services.AddHttpClient<IGameApiClient, GameApiClient>();
var app = builder.Build();

var gameProvidersService = app.Services.GetRequiredService<IGameProvidersService>();
var gameProvidersOptions = builder.Configuration.GetSection("GameProviders").Get<IEnumerable<GameProvidersOperator>>()!;
var gameProviders = gameProvidersService.Get(gameProvidersOptions);
Console.WriteLine("Game Provider Code:{0}", gameProviders);

var configurationService = app.Services.GetRequiredService<IConfigurationService>();
var configuration = await configurationService
    .GetAsync(
    gameProviders.JsonFile)
    .ConfigureAwait(false);
Console.WriteLine("configuration:{0}", configuration);

Console.Write("日期(yyyy-MM-dd):");
var dateTime = Convert.ToDateTime(Console.ReadLine() ?? throw new NullReferenceException());

var dateTypesOperators = builder.Configuration.GetSection("DataTypes").Get<IEnumerable<DateTypesOperator>>()!;
var dateTypesService = app.Services.GetRequiredService<IDateTypesService>();
var dateTypes = dateTypesService.Get(dateTypesOperators);
Console.WriteLine("date type:{0}", dateTypes);

var _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
var startTime = new DateTimeOffset(dateTime, _timeZone.GetUtcOffset(dateTime));
var endTime = startTime.AddDays(1);
var client = app.Services.GetRequiredService<IGameApiClient>();
while (true)
{
    if (startTime.CompareTo(endTime) >= 0)
    {
        break;
    }
    var data = configuration;
    var tempStartTime = startTime;
    var tempEndTime = startTime.AddSeconds(dateTypes.Seconds);
    data["startTime"] = tempStartTime;
    data["endTime"] = tempEndTime;

    var httpStatusCode = await client
        .PostAsync(
             builder.Configuration.GetSection("ServiceUrl").Get<Uri>()!,
             gameProviders.Path,
            data)
        .ConfigureAwait(false);

    Console.WriteLine($"時間:{DateTime.Now:O} 區間:{tempStartTime:O}~{tempEndTime:O} HttpStatus:{httpStatusCode}");
    startTime = startTime.AddSeconds(dateTypes.Seconds);

    await Task.Delay(TimeSpan.FromSeconds(30)).ConfigureAwait(false);


}