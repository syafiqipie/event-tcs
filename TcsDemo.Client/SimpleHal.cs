using Microsoft.Extensions.Logging;
using TcsDemo.Server;

namespace TcsDemo.Client;

public class SimpleHal(DummyFirmware fw)
{
	private ILogger? _logger;

	public void SetLogger(ILoggerFactory loggerFactory)
	{
		_logger = loggerFactory.CreateLogger<SimpleHal>();
	}

	private readonly DummyFirmware _fw = fw;

	public Task MoveMotorAsync(int pos)
	{
		TaskCompletionSource<int> tcs = new();

		void OnceCompleted(object? _, MotorDoneEventArgs args)
		{
			_logger?.LogInformation("The motor has moved to {args.Pos}", pos);
			tcs.SetResult(args.Pos);
		}

		_fw.OnMotorMoveDone += OnceCompleted;

		tcs.Task.ContinueWith(_ =>
		{
			_fw.OnMotorMoveDone -= OnceCompleted;
		});

		_logger?.LogInformation("Requesting movement to {pos}", pos);
		RequestStatus status = _fw.MoveMotor(pos);

		return tcs.Task;
	}
}
