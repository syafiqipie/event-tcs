namespace TcsDemo.Server;

public class MotorDoneEventArgs(int pos)
{
	public int Pos { get; } = pos;
}
