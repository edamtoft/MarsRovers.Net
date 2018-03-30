using MarsRovers.Net.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRovers.Net.Tests.UnitTests
{
  [TestClass]
  [TestCategory(Category.UnitTest)]
  public class MapTests
  {
    [TestMethod]
    public void DetectRoverOffMapPositive()
    {
      var map = new Map(5, 5);
      var rover = new TrackedRover(0, 10, Direction.North, map);
      Assert.AreEqual(RoverStatus.OffMap, rover.Status);
    }

    [TestMethod]
    public void DetectRoverOffMapNegative()
    {
      var map = new Map(5, 5);
      var rover = new TrackedRover(-1, -1, Direction.North, map);
      Assert.AreEqual(RoverStatus.OffMap, rover.Status);
    }

    [TestMethod]
    public void DetectRoverLeavingMap()
    {
      var map = new Map(5, 5);
      var rover = new TrackedRover(0,5, Direction.North, map);
      rover.MoveForward();
      Assert.AreEqual(RoverStatus.OffMap, rover.Status);
    }

    [TestMethod]
    public void DetectCollisionOnPlacement()
    {
      var map = new Map(5, 5);
      var rover1 = new TrackedRover(0, 0, Direction.North, map);
      var rover2 = new TrackedRover(0, 0, Direction.North, map);
      Assert.AreEqual(RoverStatus.Crashed, rover1.Status);
      Assert.AreEqual(RoverStatus.Crashed, rover2.Status);
    }

    [TestMethod]
    public void DetectCollisionAfterPlacement()
    {
      var map = new Map(5, 5);
      var rover1 = new TrackedRover(0, 1, Direction.North, map);
      var rover2 = new TrackedRover(0, 0, Direction.North, map);
      rover2.MoveForward();
      Assert.AreEqual(RoverStatus.Crashed, rover1.Status);
      Assert.AreEqual(RoverStatus.Crashed, rover2.Status);
    }
  }
}
