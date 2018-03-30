using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRovers.Net
{
  /// <summary>
  /// Tracks a collection of rovers
  /// </summary>
  public sealed class Map : IMap
  {
    private readonly List<ITrackedRover> _rovers = new List<ITrackedRover>();
    private readonly double _xMax;
    private readonly double _yMax;

    public Map(double xMax, double yMax)
    {
      _xMax = xMax;
      _yMax = yMax;
    }

    public void ReportLocation(ITrackedRover rover)
    {
      if (!_rovers.Contains(rover))
      {
        _rovers.Add(rover);
      }

      var roversAtLocation = _rovers.Where(r => r.Location == rover.Location).ToList();

      if (roversAtLocation.Count > 1)
      {
        foreach (var collidedRover in roversAtLocation)
        {
          collidedRover.Status = RoverStatus.Crashed;
        }
      }

      var (x, y) = rover.Location;

      if (x < 0 || y < 0 || x > _xMax || y > _yMax)
      {
        rover.Status = RoverStatus.OffMap;
      }
    }
  }
}
