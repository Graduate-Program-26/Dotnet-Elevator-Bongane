using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Enums;
using ElevatorSim.Domain.Exceptions;

namespace ElevatorSim.Domain.Entities;

/// <summary>
/// A Base class for all elevator types.
/// Manages floor position, direction, state, and capacity; subclasses implement
/// other operations.
/// </summary>
public class ElevatorBase : IElevator
{
    private static int s_nextId;
    /// <inheritdoc/>
    public int Id { get; } = Interlocked.Increment(ref s_nextId);

    /// <inheritdoc/>
    public int CurrentFloorNumber { get; private set; }

    /// <inheritdoc/>
    public int CurrentCapacity { get; private set; }

    /// <inheritdoc/>
    public int MaxCapacity { get; init; }

    /// <inheritdoc/>
    public ElevatorState State { get; private set; }

    /// <inheritdoc/>
    public ElevatorDirection Direction { get; private set; }

    /// <inheritdoc/>
    public int MinFloor { get; init; }

    /// <inheritdoc/>
    public int MaxFloor { get; init; }

    /// <summary>
    /// Initialises a new elevator with the specified floor bounds, capacity, and starting position.
    /// </summary>
    /// <param name="minFloor">The lowest floor this elevator services.</param>
    /// <param name="maxFloor">The highest floor this elevator services.</param>
    /// <param name="maxCapacity">The maximum number of passengers this elevator can carry.</param>
    /// <param name="floorNumber">The floor the elevator starts on.</param>
    /// <param name="state">The initial operational state; defaults to <see cref="ElevatorState.Stationary"/>.</param>
    /// <param name="direction">The initial travel direction; defaults to <see cref="ElevatorDirection.Idle"/>.</param>
    public ElevatorBase(int minFloor, int maxFloor, int maxCapacity, int floorNumber,
        ElevatorState state = ElevatorState.Stationary,
        ElevatorDirection direction = ElevatorDirection.Idle)
    {
        CurrentFloorNumber = floorNumber;
        MinFloor = minFloor;
        MaxFloor = maxFloor;
        MaxCapacity = maxCapacity;
        CurrentCapacity = maxCapacity;
        State = state;
        Direction = direction;
    }

    /// <inheritdoc/>
    public void ChangeState(ElevatorState state)
    {
        State = state;
    }

    /// <inheritdoc/>
    public void Board(int load)
    {
        if (this.State == ElevatorState.Moving)
        {
            throw new ElevatorMovingException(this.Id);
        }

        if (this.State == ElevatorState.OutOfService)
        {
            throw new ElevatorOutOfServiceException(this.Id);
        }
        
        if (load > this.CurrentCapacity)
        {
            throw new CapacityExceededException(this.Id,this.CurrentCapacity);
        }
        
        this.CurrentCapacity -= load;
    }

    /// <inheritdoc/>
    public void Step(int targetFloor)
    {
        if (State != ElevatorState.Moving)
            return;

        if (targetFloor > MaxFloor)
        {
            throw new ElevatorExceedsAvailableFloorsException(MaxFloor, MaxFloor);
        }
        
        if (targetFloor < MinFloor)
        {
            throw new ElevatorExceedsAvailableFloorsException(MinFloor, MaxFloor);
        }
        
        if (CurrentFloorNumber < targetFloor)
        {
            CurrentFloorNumber++;
            this.Direction = ElevatorDirection.Up;
        }
        else if (CurrentFloorNumber > targetFloor)
        {
            CurrentFloorNumber--;
            this.Direction = ElevatorDirection.Down;
        }
        
        if (CurrentFloorNumber == targetFloor)
        {
            State = ElevatorState.Stationary;
            Direction = ElevatorDirection.Idle;
        }
    }
}
