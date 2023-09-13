namespace NextDns.LinkedIp.Updater;

public class BackgroundUpdaterHostedService : IHostedService
{
    private Task _executingTask;
    private readonly CancellationTokenSource _cts = new CancellationTokenSource();
    private readonly IUpdater _updater;
    private readonly  ILogger<BackgroundUpdaterHostedService> _logger;

    public BackgroundUpdaterHostedService( 
        IUpdater updater,
        ILogger<BackgroundUpdaterHostedService> logger)
    {
        _updater = updater;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting background updater");
        _executingTask = ExecuteAsync(_cts.Token);
        return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        if (_executingTask == null)
        {
            return Task.CompletedTask;
        }
      
        _logger.LogInformation("Stopping background updater");
        _cts.Cancel();

        return Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await _updater.UpdateAsync(cancellationToken);
            await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);
        }
    }

    public void Dispose() => _cts.Cancel();
}