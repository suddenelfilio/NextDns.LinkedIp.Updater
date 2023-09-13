using Microsoft.AspNetCore.Mvc;

namespace NextDns.LinkedIp.Updater.Controllers;

[ApiController]
[Route("[controller]")]
public class LinkedIpController : ControllerBase
{
  private readonly ILogger<LinkedIpController> _logger;
  private readonly IUpdater _updater;

  public LinkedIpController(ILogger<LinkedIpController> logger, IUpdater updater)
  {
      _logger = logger;
      _updater = updater;
  }

    [HttpGet()]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        await _updater.UpdateAsync(cancellationToken);
        return Ok();
    }
}