using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRovers.Net
{
  /// <summary>
  /// A rover with a status that can be set by an outside entity
  /// </summary>
  public interface ITrackedRover : IRover
  {
    RoverStatus Status { get; set; }
  }
}
