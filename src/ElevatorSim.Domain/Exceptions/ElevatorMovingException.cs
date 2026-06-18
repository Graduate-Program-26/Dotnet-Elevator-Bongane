namespace ElevatorSim.Domain.Exceptions;

/// <summary>
/// Thrown when an operation is attempted on an elevator that is currently in motion.
/// </summary>
public class ElevatorMovingException : Exception
{
    /// <summary>
    /// Initialises a new <see cref="ElevatorMovingException"/>.
    /// </summary>
    /// <param name="elevatorId">The identifier of the moving elevator.</param>
    public ElevatorMovingException(int elevatorId)
        : base($"Elevator {elevatorId} is moving.")
    {
    }
}
