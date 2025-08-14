using Microsoft.Extensions.Logging;

namespace TcsDemo.Server;

public class DummyFirmware : BaseDummyFirmware
{
	public event EventHandler<StepperMotorMoveDoneEventArgs>? OnStepperMotorMoveDone;
	public event EventHandler<StepperMotorHomeDoneEventArgs>? OnStepperMotorHomeDone;

	public uint MoveMotor(int id, int pos, int vel, int acc)
	{
		_logger?.LogInformation("Movement to {pos} requested", pos);

		Thread thread = new(() => MoveMotorInternal(id, pos, vel, acc));
		thread.Start();
		return 0;
	}

	public uint HomeMotor(int id)
	{
		_logger?.LogInformation("Homing requested");

		Thread thread = new(() => HomeMotorInternal(id));
		thread.Start();
		return 0;
	}

	private void MoveMotorInternal(int id, int pos, int vel, int acc)
	{
		_logger?.LogInformation("Starting movement to {pos}", pos);
		Thread.Sleep(5000); // Simulate work
		_logger?.LogInformation("Movement to {pos} done", pos);

		StepperMotorMoveDoneEventArgs e = new(id, pos, 0);
		OnStepperMotorMoveDone?.Invoke(this, e);
	}

	private void HomeMotorInternal(int id)
	{
		_logger?.LogInformation("Starting homing");
		Thread.Sleep(5000); // Simulate work
		_logger?.LogInformation("Homing done");

		StepperMotorHomeDoneEventArgs eventArgs = new(0);
		OnStepperMotorHomeDone?.Invoke(this, eventArgs);
	}
}
