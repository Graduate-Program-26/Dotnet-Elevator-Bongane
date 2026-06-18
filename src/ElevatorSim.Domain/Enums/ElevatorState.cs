namespace ElevatorSim.Domain.Enums;

/// <summary>
/// Represents the operational state of an elevator at a point in time.
/// </summary>
public enum ElevatorState
{
    /// <summary>The elevator is stopped and idle at a floor.</summary>
    Stationary,

    /// <summary>The elevator is traveling between floors.</summary>
    Moving,

    /// <summary>The elevator doors are open and passengers may board.</summary>
    DoorsOpen,

    /// <summary>The elevator is taken out of service and unavailable for dispatch.</summary>
    OutOfService,

    /// <summary>The elevator doors have closed after a boarding cycle.</summary>
    DoorsClosed
}
