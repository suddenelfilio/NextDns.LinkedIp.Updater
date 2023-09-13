using NextDns.LinkedIp.Updater;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();
builder.Services.AddTransient<IUpdater, NextDnsLinkedIpUpdater>();
builder.Services.AddHostedService<BackgroundUpdaterHostedService>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

app.Run();