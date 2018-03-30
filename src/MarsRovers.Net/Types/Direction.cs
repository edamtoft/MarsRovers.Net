using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRovers.Net.Types
{
  /// <summary>
  /// Represents a direction as degrees from north
  /// </summary>
  public struct Direction : IEquatable<Direction>, IComparable<Direction>
  {
    private int _degrees;
    public Direction(int degrees)
    {
      var mod = degrees % 360;
      _degrees = mod >= 0 ? mod : mod + 360;
    }

    public Direction Left => new Direction(_degrees - 90);
    public Direction Right => new Direction(_degrees + 90);

    public static Direction North => new Direction(0);
    public static Direction East => new Direction(90);
    public static Direction South => new Direction(180);
    public static Direction West => new Direction(270);

    public double AsRadian() => (Math.PI / 180) * _degrees;

    #region Overrides / Interface Methods
    public override bool Equals(object obj) => obj is Direction dir && Equals(dir);
    public bool Equals(Direction other) => _degrees == other._degrees;
    public override int GetHashCode() => _degrees.GetHashCode();
    public int CompareTo(Direction other) => _degrees.CompareTo(other._degrees);

    public override string ToString()
    {
      switch (_degrees)
      {
        case 0:
          return "N";
        case 90:
          return "E";
        case 180:
          return "S";
        case 270:
          return "W";
        default:
          return _degrees + "deg";
      }
    }
    #endregion

    #region Operators
    public static bool operator ==(Direction dir1, Direction dir2) => dir1.Equals(dir2);
    public static bool operator !=(Direction dir1, Direction dir2) => !(dir1 == dir2);
    public static Vector operator *(Direction direction, int distance) => new Vector(direction, distance);

    public static implicit operator Direction(int deg) => new Direction(deg);
    public static implicit operator int(Direction dir) => dir._degrees;
    #endregion
  }
}
