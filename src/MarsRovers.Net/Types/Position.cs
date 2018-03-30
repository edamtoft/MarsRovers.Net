using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRovers.Net.Types
{
  /// <summary>
  /// Represents a coordinate position
  /// </summary>
  public struct Position : IEquatable<Position>
  {
    private readonly (double x, double y) _coords;

    public Position(double x, double y)
    {
      _coords = (x, y);
    }

    public void Deconstruct(out double x, out double y)
    {
      x = _coords.x;
      y = _coords.y;
    }

    #region Overrides / Interface Methods
    public override bool Equals(object obj) => obj is Position pos && Equals(pos);
    public bool Equals(Position other) => _coords.Equals(other._coords);
    public override int GetHashCode() => _coords.GetHashCode();
    public override string ToString() => $"{_coords.x} {_coords.y}";
    #endregion

    #region Operators
    public static bool operator ==(Position pos1, Position pos2) => pos1.Equals(pos2);
    public static bool operator !=(Position pos1, Position pos2) => !(pos1 == pos2);

    public static implicit operator Position((double x, double y) coords) => new Position(coords.x, coords.y);
    #endregion
  }
}
