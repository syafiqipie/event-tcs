using Microsoft.Extensions.Logging;

namespace TcsDemo.Server;

public class DummyFirmware()
{
	private ILogger? _logger;
	public event EventHandler<MotorDoneEventArgs>? OnMotorMoveDone;

	public void SetLogger(ILoggerFactory loggerFactory)
	{
		_logger = loggerFactory.CreateLogger<DummyFirmware>();
	}

	public RequestStatus MoveMotor(int pos)
	{
		Thread.Sleep(100); // Simulate latency
		_logger?.LogInformation("Movement to {pos} requested", pos);

		Thread thread = new(() => MoveMotorInternal(pos));
		thread.Start();
		return RequestStatus.Success;
	}

	private void MoveMotorInternal(int pos)
	{
		_logger?.LogInformation("Starting movement to {pos}", pos);
		Thread.Sleep(5000); // Simulate work
		_logger?.LogInformation("Movement to {pos} done", pos);

		MotorDoneEventArgs e = new(pos);
		OnMotorMoveDone?.Invoke(this, e);
	}
}
