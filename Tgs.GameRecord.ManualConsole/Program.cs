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
builder.Services.AddTransient<ITimeRangeService, TimeRangeService>();
builder.Services.AddTransient<IDateModesService, DateModesService>();
builder.Services.AddTransient<IDateTypesService, DateTypesService>();
builder.Services.AddTransient<ITimeIntervalsService, TimeIntervalsService>();
builder.Services.AddHttpClient<IGameApiClient, GameApiClient>();
var app = builder.Build();

var gameProvidersService = app.Services.GetRequiredService<IGameProvidersService>();
var gameProvidersOptions = builder.Configuration.GetSection("GameProviders").Get<IEnumerable<GameProvidersOperator>>()!;
var gameProvider = gameProvidersService.Get(gameProvidersOptions);

var configurationService = app.Services.GetRequiredService<IConfigurationService>();
var configuration = await configurationService
    .GetAsync(
    gameProvider.JsonFile)
    .ConfigureAwait(false);

var dateModesService = app.Services.GetRequiredService<IDateModesService>();
var dateMode = dateModesService.Get();

var timeRangeService = app.Services.GetRequiredService<ITimeRangeService>();
(DateTimeOffset StartTime, DateTimeOffset EndTime) = timeRangeService.Get(dateMode);

var dateTypesOperators = builder.Configuration.GetSection("DataTypes").Get<IEnumerable<DateTypesOperator>>()!;
var dateTypesService = app.Services.GetRequiredService<IDateTypesService>();
var dateType = dateTypesService.Get(dateTypesOperators);

var timeIntervalsOperators = builder.Configuration.GetSection("TimeIntervals").Get<IEnumerable<TimeIntervalsOperator>>()
    ?? new TimeIntervalsOperator[]{
        new TimeIntervalsOperator
        {
            Name="每30秒",
            Seconds=30
        }
    };
var timeIntervalsService = app.Services.GetRequiredService<ITimeIntervalsService>();
var timeInterval = timeIntervalsService.Get(timeIntervalsOperators);

var client = app.Services.GetRequiredService<IGameApiClient>();
while (true)
{
    if (StartTime.CompareTo(EndTime) >= 0)
    {
        break;
    }
    var data = configuration;
    var tempStartTime = StartTime;
    var tempEndTime = StartTime.AddSeconds(dateType.Seconds);
    data["startTime"] = tempStartTime;
    data["endTime"] = tempEndTime;

    var httpStatusCode = await client
        .PostAsync(
             builder.Configuration.GetSection("ServiceUrl").Get<Uri>()!,
             gameProvider.Path,
            data)
        .ConfigureAwait(false);

    Console.WriteLine($"時間:{DateTime.Now:O} 區間:{tempStartTime:O}~{tempEndTime:O} HttpStatus:{httpStatusCode}");
    StartTime = StartTime.AddSeconds(dateType.Seconds);

    await Task.Delay(TimeSpan.FromSeconds(timeInterval.Seconds)).ConfigureAwait(false);
}
