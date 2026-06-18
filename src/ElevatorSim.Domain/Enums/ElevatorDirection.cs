namespace ElevatorSim.Domain.Enums;

/// <summary>
/// Represents the direction an elevator is currently travelling.
/// </summary>
public enum ElevatorDirection
{
    /// <summary>The elevator is moving toward higher-numbered floors.</summary>
    Up,

    /// <summary>The elevator is moving toward lower-numbered floors.</summary>
    Down,

    /// <summary>The elevator is stationary and not committed to a direction.</summary>
    Idle
}
