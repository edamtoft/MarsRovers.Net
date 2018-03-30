using MarsRovers.Net.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRovers.Net.Tests.UnitTests
{
  [TestClass]
  [TestCategory(Category.UnitTest)]
  public class TypeSystemTests
  {
    [TestMethod]
    public void DirectionLeftAndRight()
    {
      var direction = Direction.North;

      Assert.AreEqual(Direction.West, direction.Left);
      Assert.AreEqual(Direction.East, direction.Right);
    }

    [TestMethod]
    public void DirectionEquality()
    {
      Assert.AreEqual(Direction.North, new Direction(0));
      Assert.AreEqual(Direction.East, new Direction(90));
      Assert.AreEqual(Direction.South, new Direction(180));
      Assert.AreEqual(Direction.West, new Direction(270));
      Assert.AreEqual(Direction.North, default(Direction));
    }

    [TestMethod]
    public void DirectionConversions()
    {
      var direction = Direction.East;
      Assert.AreEqual<int>(90, direction);
    }

    [TestMethod]
    public void DirectionToString()
    {
      Assert.AreEqual("N", Direction.North.ToString());
      Assert.AreEqual("E", Direction.East.ToString());
      Assert.AreEqual("S", Direction.South.ToString());
      Assert.AreEqual("W", Direction.West.ToString());
    }

    [TestMethod]
    public void DirectionToVector()
    {
      var vector = Direction.North * 1;
      Assert.AreEqual(new Vector(Direction.North, 1), vector);
    }

    [TestMethod]
    public void PositionEquality()
    {
      Assert.AreEqual(new Position(1, 2), new Position(1, 2));
      Assert.AreEqual(new Position(0, 0), default(Position));
    }

    [TestMethod]
    public void PositionPlusVector()
    {
      var oneNorth = Direction.North * 1;
      var positionToTheNorth = new Position(0, 0) + oneNorth;
      Assert.AreEqual(new Position(0, 1), positionToTheNorth);
    }

    [TestMethod]
    public void PositionDestructuring()
    {
      var (x, y) = new Position(1, 2);
      Assert.AreEqual(1, x);
      Assert.AreEqual(2, y);
    }
  }
}
