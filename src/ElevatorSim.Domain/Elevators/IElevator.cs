namespace ElevatorSim.Domain.Elevators;

public interface IElevator
{
    int Id { get; }
    int Floor { get; }
    ElevatorState State { get;  }
    ElevatorDirection Direction { get; }
    
}