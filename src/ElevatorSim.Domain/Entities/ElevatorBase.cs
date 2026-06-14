using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Enums;

namespace ElevatorSim.Domain.Entities;

public abstract class ElevatorBase : IElevator,
    IEntity
{
    private Guid _id;
    public int FloorNumber { get; private set; }
    public ElevatorState State { get; private set; }
    public ElevatorDirection Direction { get; private set; }
    public int MinFloor { get; init; }
    public int MaxFloor { get; init; }

    public ElevatorBase(int floorNumber = 0, int minFloor = 0)
    {
        
    }
    Guid IEntity.Id => _id;
}