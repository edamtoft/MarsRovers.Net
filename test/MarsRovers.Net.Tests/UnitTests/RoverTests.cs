using MarsRovers.Net.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MarsRovers.Net.Tests.UnitTests
{
  [TestClass]
  [TestCategory(Category.UnitTest)]
  public class RoverTests
  {
    [TestMethod]
    public void LeftTurn()
    {
      var rover = new Rover(1, 2, Direction.North);
      rover.TurnLeft();
      Assert.AreEqual(Direction.West, rover.Heading);
    }

    [TestMethod]
    public void RightTurn()
    {
      var rover = new Rover(1, 2, Direction.North);
      rover.TurnRight();
      Assert.AreEqual(Direction.East, rover.Heading);
    }

    [TestMethod]
    public void FullCircleRight()
    {
      var rover = new Rover(1, 2, Direction.North);
      rover.TurnRight();
      rover.TurnRight();
      rover.TurnRight();
      rover.TurnRight();
      Assert.AreEqual(Direction.North, rover.Heading);
    }

    [TestMethod]
    public void FullCircleLeft()
    {
      var rover = new Rover(1, 2, Direction.North);
      rover.TurnLeft();
      rover.TurnLeft();
      rover.TurnLeft();
      rover.TurnLeft();
      Assert.AreEqual(Direction.North, rover.Heading);
    }

    [TestMethod]
    public void ForwardMovement()
    {
      var rover = new Rover(1, 2, Direction.North);
      rover.MoveForward();
      Assert.AreEqual((1, 3), rover.Location);
    }

    public void AsString()
    {
      var rover = new Rover(1, 2, Direction.North);
      Assert.AreEqual("1 2 N", rover.ToString());
    }


    [TestMethod]
    public void SampleInput()
    {
      var rover1 = new Rover(1, 2, Direction.North);
      "LMLMLMLMM".Aggregate(rover1, PerformCommand);
      var rover2 = new Rover(3, 3, Direction.East);
      "MMRMMRMRRM".Aggregate(rover2, PerformCommand);

      Assert.AreEqual("1 3 N", rover1.ToString());
      Assert.AreEqual("5 1 E", rover2.ToString());
    }

    private static Rover PerformCommand(Rover rover, char command)
    {
      switch (command)
      {
        case 'L':
          rover.TurnLeft();
          break;
        case 'R':
          rover.TurnRight();
          break;
        case 'M':
          rover.MoveForward();
          break;
      }
      return rover;
    }
  }
}
