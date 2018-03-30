using MarsRovers.Net.Types;
using System;

namespace MarsRovers.Net
{
  public interface IRover
  {
    Direction Heading { get; }
    Position Location { get; }

    void MoveForward();
    void TurnLeft();
    void TurnRight();
  }
}