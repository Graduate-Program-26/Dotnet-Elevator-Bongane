using ElevatorSim.Application.Interfaces;
using ElevatorSim.Application.Models;
using ElevatorSim.Domain.Enums;
using ElevatorSim.Domain.Exceptions;
using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Models;

namespace ElevatorSim.Application.Services;

/// <summary>
/// Coordinates the elevator fleet for a building: accepts floor requests,
/// selects the best elevator via an <see cref="IDispatchStrategy"/>, and manages
/// the full dispatch lifecycle (moving → doors open → boarding → doors closed).
/// </summary>
public class ElevatorController : IElevatorController
{
    private readonly int _maxNumberOfFloors;
    private readonly int _maxNumberOfElevators;
    private readonly IDispatchStrategy _strategy;

    /// <summary>Gets the elevators currently registered with this controller.</summary>
    public List<IElevator> Elevators { get; } = new List<IElevator>();

    /// <inheritdoc/>
    IReadOnlyList<IElevator> IElevatorController.Elevators => Elevators;
    
    /// <summary>
    /// Initialises the controller with a single elevator.
    /// </summary>
    /// <param name="maxNumberOfFloors">The highest floor in the building.</param>
    /// <param name="maxNumberOfElevators">The maximum number of elevators allowed.</param>
    /// <param name="elevator">The initial elevator to register.</param>
    /// <param name="strategy">The dispatch strategy used to select elevators.</param>
    /// <exception cref="ElevatorExceedsAvailableFloorsException">
    /// Thrown when the elevator's <see cref="IElevator.MaxFloor"/> exceeds <paramref name="maxNumberOfFloors"/>.
    /// </exception>
    public ElevatorController(int maxNumberOfFloors, int maxNumberOfElevators,
        IElevator elevator, IDispatchStrategy strategy)
    {
        _maxNumberOfFloors = maxNumberOfFloors;
        _maxNumberOfElevators = maxNumberOfElevators;
        ValidateElevatorCapacity(elevator);
        Elevators.Add(elevator);
        _strategy = strategy;
    }

    /// <summary>
    /// Initialises the controller with a fleet of elevators.
    /// </summary>
    /// <param name="maxNumberOfFloors">The highest floor in the building.</param>
    /// <param name="maxNumberOfElevators">The maximum number of elevators allowed.</param>
    /// <param name="elevators">The initial fleet of elevators to register.</param>
    /// <param name="strategy">The dispatch strategy used to select elevators.</param>
    /// <exception cref="ElevatorExceedsAvailableFloorsException">
    /// Thrown when any elevator's <see cref="IElevator.MaxFloor"/> exceeds <paramref name="maxNumberOfFloors"/>.
    /// </exception>
    public ElevatorController(int maxNumberOfFloors, int maxNumberOfElevators,
        List<IElevator> elevators, IDispatchStrategy strategy)
    {
        _maxNumberOfFloors = maxNumberOfFloors;
        _maxNumberOfElevators = maxNumberOfElevators;
        elevators.ForEach(ValidateElevatorCapacity);
        Elevators.AddRange(elevators);
        _strategy = strategy;
    }

    /// <summary>
    /// Adds an elevator to the fleet.
    /// </summary>
    /// <param name="elevator">The elevator to add.</param>
    /// <exception cref="BuildingElevatorCapacityExceededException">
    /// Thrown when the fleet is already at <c>maxNumberOfElevators</c>.
    /// </exception>
    /// <exception cref="ElevatorExceedsAvailableFloorsException">
    /// Thrown when the elevator's max floor exceeds the building floor count.
    /// </exception>
    public void AddElevators(IElevator elevator)
    {
        if (Elevators.Count >= _maxNumberOfElevators)
        {
            throw new BuildingElevatorCapacityExceededException(_maxNumberOfElevators);
        }

        ValidateElevatorCapacity(elevator);
        Elevators.Add(elevator);
    }
    

    /// <summary>
    /// Dispatches one or more elevators to service the given floor request.
    /// The method loops until all passengers are boarded or fleet capacity is exhausted.
    /// </summary>
    /// <param name="fRequest">The floor and passenger count to service.</param>
    /// <param name="progress">
    /// Optional progress sink for UI updates during the dispatch cycle.
    /// Pass <see langword="null"/> (or omit) to run silently.
    /// </param>
    /// <returns>
    /// An <see cref="ElevatorDispatchResult"/> containing the number of passengers boarded
    /// and the number still waiting.
    /// </returns>
    /// <exception cref="FloorRequestExceedsAvailableFloorsException">
    /// Thrown when <paramref name="fRequest"/>'s floor number exceeds the building maximum.
    /// </exception>
    public ElevatorDispatchResult DispatchElevator(FloorRequest fRequest, IDispatchProgress? progress = null)
    {
        int numberOfPassengersReqestingElevator = fRequest.NumberOfPassengers;
        int totalCapacityAvailable = Elevators.Sum(elevator => elevator.CurrentCapacity);
        int passengersBoarded = 0;
        while (numberOfPassengersReqestingElevator > 0 && totalCapacityAvailable > 0)
        {
            var nextBestChoice = _strategy.DispatchElevator(fRequest, Elevators);

            if (nextBestChoice == null)
            {
                return new ElevatorDispatchResult(passengersBoarded, numberOfPassengersReqestingElevator);
            }
            
            progress?.ElevatorDispatched(nextBestChoice.Id, nextBestChoice.CurrentFloorNumber, fRequest.FloorNumber);

            if (fRequest.FloorNumber > _maxNumberOfFloors)
            {
                throw new FloorRequestExceedsAvailableFloorsException(fRequest.FloorNumber, _maxNumberOfFloors);
            }

            nextBestChoice.ChangeState(ElevatorState.Moving);
            progress?.ElevatorChangedState(nextBestChoice.Id, nextBestChoice.State);

            while (nextBestChoice.State == ElevatorState.Moving)
            {
                nextBestChoice.Step(fRequest.FloorNumber);
                progress?.ElevatorMoved();
            }

            nextBestChoice.ChangeState(ElevatorState.DoorsOpen);
            progress?.ElevatorChangedState(nextBestChoice.Id, nextBestChoice.State);

            while (nextBestChoice.CurrentCapacity > 0)
            {
                if (numberOfPassengersReqestingElevator <= 0)
                    break;
                nextBestChoice.Board(1);
                progress?.PassengersBoarded(1, nextBestChoice.CurrentCapacity);
                --numberOfPassengersReqestingElevator;
            }

            nextBestChoice.ChangeState(ElevatorState.DoorsClosed);
            progress?.ElevatorChangedState(nextBestChoice.Id, nextBestChoice.State);

            totalCapacityAvailable = Elevators.Sum(elevator => elevator.CurrentCapacity);

            passengersBoarded = fRequest.NumberOfPassengers - numberOfPassengersReqestingElevator;
        }

        return new ElevatorDispatchResult(passengersBoarded, numberOfPassengersReqestingElevator);
    }

    private void ValidateElevatorCapacity(IElevator elevator)
    {
        if (elevator.MaxFloor > _maxNumberOfFloors)
        {
            throw new ElevatorExceedsAvailableFloorsException(elevator.MaxFloor, _maxNumberOfFloors);
        }
    }
}
