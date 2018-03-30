using MarsRovers.Net.Types;
using System;

namespace MarsRovers.Net
{
  /// <summary>
  /// A mars rover that can move and turn on a two dimensional grid
  /// </summary>
  public class Rover : IRover
  {
    public Rover(double x, double y, Direction heading)
    {
      Location = (x, y);
      Heading = heading;
    }

    public Direction Heading { get; private set; }
    public Position Location { get; private set; }

    public virtual void TurnLeft()
    {
      Heading = Heading.Left;
    }

    public virtual void TurnRight()
    {
      Heading = Heading.Right;
    }

    public virtual void MoveForward()
    {
      Location += (Heading * 1);
    }

    public override string ToString() => $"{Location} {Heading}";
  }
}
