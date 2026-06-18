namespace ElevatorSim.Domain.Exceptions;

/// <summary>
/// Thrown when an operation is attempted on an elevator that is out of service.
/// </summary>
public class ElevatorOutOfServiceException : Exception
{
    /// <summary>
    /// Initialises a new <see cref="ElevatorOutOfServiceException"/>.
    /// </summary>
    /// <param name="elevatorId">The identifier of the out-of-service elevator.</param>
    public ElevatorOutOfServiceException(int elevatorId)
        : base($"Elevator {elevatorId} is out of service.")
    {
    }
}
