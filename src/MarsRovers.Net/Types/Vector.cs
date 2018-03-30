using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRovers.Net.Types
{
  /// <summary>
  /// Represents a distance * direction
  /// </summary>
  public struct Vector : IEquatable<Vector>
  {
    private readonly (Direction direction, int distance) _value;

    public Vector(Direction direction, int distance)
    {
      _value = (direction, distance);
    }

    public void Deconstruct(out Direction direction, out int distance)
    {
      direction = _value.direction;
      distance = _value.distance;
    }

    #region Overrides / Interface Methods
    public override bool Equals(object obj) => obj is Vector vec && Equals(vec);
    public bool Equals(Vector other) => _value.Equals(other._value);
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => $"{_value.direction}*{_value.distance}";
    #endregion

    #region Operators
    public static bool operator ==(Vector vector1, Vector vector2) => vector1.Equals(vector2);
    public static bool operator !=(Vector vector1, Vector vector2) => !(vector1 == vector2);
    public static Position operator +(Position position, Vector vector)
    {
      var (x, y) = position;
      var (direction, distance) = vector;
      var theta = direction.AsRadian();
      var x1 = x + Math.Round(Math.Sin(theta) * distance, 4);
      var y1 = y + Math.Round(Math.Cos(theta) * distance, 4);
      return (x1, y1);
    }
    #endregion
  }
}
