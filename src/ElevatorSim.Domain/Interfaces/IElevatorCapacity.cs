namespace ElevatorSim.Domain.Interfaces;

public interface IElevatorCapacity
{
    

    /// <summary>Gets the number of additional passengers the elevator can still accept.</summary>
    int CurrentCapacity { get; }

    /// <summary>Gets the maximum number of passengers this elevator can carry.</summary>
    int MaxCapacity { get; }
    /// <summary>Gets the highest floor this elevator can reach.</summary>
    int MaxFloor { get; }

    /// <summary>Gets the lowest floor this elevator can reach.</summary>
    int MinFloor { get; }
}