using MarsRovers.Net.Types;

namespace MarsRovers.Net
{
  /// <summary>
  /// Decorates a <see cref="IRover"/> to report it's position to mission control, and will stop moving if set
  /// to a non-active <see cref="RoverStatus"/>.
  /// </summary>
  public sealed class TrackedRover : Rover, ITrackedRover
  {
    private readonly IMap _map;

    public TrackedRover(double x, double y, Direction direction, IMap map) : base(x,y,direction)
    {
      _map = map;
      _map.ReportLocation(this);
    }

    public RoverStatus Status { get; set; }

    public override void MoveForward()
    {
      if (Status == RoverStatus.Active)
      {
        base.MoveForward();
        _map.ReportLocation(this);
      }
    }

    public override void TurnLeft()
    {
      if (Status == RoverStatus.Active)
      {
        base.TurnLeft();
      }
    }

    public override void TurnRight()
    {
      if (Status == RoverStatus.Active)
      {
        base.TurnRight();
      }
    }

    public override string ToString()
    {
      return Status == RoverStatus.Active 
        ? base.ToString()
        : $"{base.ToString()} ({Status})";
    }
  }
}
