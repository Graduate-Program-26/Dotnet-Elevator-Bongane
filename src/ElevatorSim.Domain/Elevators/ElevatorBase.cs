namespace ElevatorSim.Domain.Elevators;

public abstract class ElevatorBase : IElevator
{
    public int Id { get; }
    public int Floor { get; private set; }
    public ElevatorState State { get; private set; }
    public ElevatorDirection Direction { get; private set; }
}