using System.Net;
using Flurl.Http;

namespace NextDns.LinkedIp.Updater;

public interface IUpdater
{
    public Task UpdateAsync(CancellationToken cancellationToken = default);
}

public class NextDnsLinkedIpUpdater : IUpdater
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<NextDnsLinkedIpUpdater> _logger;

    public NextDnsLinkedIpUpdater(IConfiguration configuration, ILogger<NextDnsLinkedIpUpdater> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task UpdateAsync(CancellationToken cancellationToken = default)
    {
        var updateLink = _configuration.GetValue<string>("nextdns:updateLink");
        if (string.IsNullOrEmpty(updateLink))
        {
            _logger.LogWarning("Nextdns update link is not configured");
            return;
        }

        _logger.LogInformation("Updating linked ip via url: {0}", updateLink);
        var response = await updateLink.GetAsync(cancellationToken);
        if (response.StatusCode == 200)
            _logger.LogInformation("Linked ip updated successfully");
        else _logger.LogInformation("Linked ip update failed with status code: {0}", response.StatusCode);
    }
}