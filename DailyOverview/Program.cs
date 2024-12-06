using DailyOverview;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = ".NET Joke Service";
});
builder.Services.AddHostedService<Worker>();
builder.Services.AddSerilog(config =>
{
    config.ReadFrom.Configuration(builder.Configuration);
});

var host = builder.Build();
host.Run();
