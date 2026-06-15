using ElevatorSim.Domain.Models;

namespace ElevatorSim.Domain.Interfaces;

public interface IDispatchStrategy
{
    public IElevator DispatchElevator(FloorRequest floorRequest, List<IElevator> elevators);
}