namespace ElevatorSim.Domain.Exceptions;

public class ElevatorMovingException : Exception
{
    public ElevatorMovingException(Guid elevatorId) : base($"Elevator {elevatorId} is moving.")
    {
      
    }
}