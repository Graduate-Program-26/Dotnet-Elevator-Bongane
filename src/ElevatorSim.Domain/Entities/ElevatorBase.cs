using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Enums;

namespace ElevatorSim.Domain.Entities;

public abstract class ElevatorBase : IElevator
{
    public int Id { get; }
    public int Floor { get; private set; }
    public ElevatorState State { get; private set; }
    public ElevatorDirection Direction { get; private set; }
}