namespace ElevatorSim.Domain.Exceptions;

public class ElevatorOutOfServiceException : Exception
{
    public ElevatorOutOfServiceException(Guid elevatorId) : base($"Elevator {elevatorId} is out of service.")
    {
      
    }
}