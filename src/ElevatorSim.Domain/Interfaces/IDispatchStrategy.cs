using ElevatorSim.Domain.Models;

namespace ElevatorSim.Domain.Interfaces;

public interface IDispatchStrategy
{
    List<IElevator> Elevators { get; }
    Queue<FloorRequest> FloorRequests { get; }
    public IElevator DispatchElevator(FloorRequest floorRequest);
}