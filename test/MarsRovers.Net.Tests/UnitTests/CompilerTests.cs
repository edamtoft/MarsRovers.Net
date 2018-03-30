using MarsRovers.Net.Compiler;
using MarsRovers.Net.Compiler.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRovers.Net.Tests.UnitTests
{
  [TestClass]
  [TestCategory(Category.UnitTest)]
  public class CompilerTests
  {
    [TestMethod]
    public void HappyPath()
    {
      var compiler = new RoverCompiler();

      compiler.DeclareMap(5, 5);
      compiler.DeclareRover(1, 2, "N");
      compiler.DeclareCommands("LMLMLMLMM");
      compiler.DeclareRover(3, 3, "E");
      compiler.DeclareCommands("MMRMMRMRRM");
      compiler.Finish();

      var rovers = compiler.Mission();

      Assert.AreEqual(2, rovers.Length);
      Assert.AreEqual("1 3 N", rovers[0].ToString());
      Assert.AreEqual("5 1 E", rovers[1].ToString());
    }

    [TestMethod]
    [ExpectedException(typeof(CompilerException))]
    public void InvalidDirections()
    {
      var compiler = new RoverCompiler();
      compiler.DeclareRover(1, 2, "Q");
    }

    [TestMethod]
    [ExpectedException(typeof(CompilerException))]
    public void InvalidCommands()
    {
      var compiler = new RoverCompiler();
      compiler.DeclareCommands("Q");
    }
  }
}
