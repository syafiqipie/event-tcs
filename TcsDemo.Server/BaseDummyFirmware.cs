using Microsoft.Extensions.Logging;

namespace TcsDemo.Server;

public abstract class BaseDummyFirmware
{
	protected ILogger? _logger;

	public void SetLogger(ILoggerFactory loggerFactory)
	{
		_logger = loggerFactory.CreateLogger<DummyFirmware>();
	}
}
