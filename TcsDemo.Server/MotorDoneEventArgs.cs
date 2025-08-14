namespace TcsDemo.Server;

public class StepperMotorMoveDoneEventArgs(int id, int pos, uint status)
{
	public int Id { get; } = id;
	public int Pos { get; } = pos;
	public uint Status { get; } = status;
}

public class StepperMotorHomeDoneEventArgs(int id, int pos)
{
	public int Id { get; } = id;
	public int Pos { get; } = pos;
}
