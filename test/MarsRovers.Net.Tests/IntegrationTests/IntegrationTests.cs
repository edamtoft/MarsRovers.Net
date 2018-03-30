using MarsRovers.Net.Compiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MarsRovers.Net.Tests.IntegrationTests
{
  [TestClass]
  [TestCategory(Category.IntegrationTest)]
  public class IntegrationTests
  {
    [TestMethod]
    public void RunTestCase()
    {
      const string TestInput = @"5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM";

      const string ExpectedOutput = @"1 3 N
5 1 E";

      var compiler = new RoverCompiler();
      var parser = new RoverParser(compiler);
      using (var reader = new StringReader(TestInput))
      {
        parser.Parse(reader);
      }

      var rovers = compiler.Mission();

      Assert.AreEqual(ExpectedOutput, string.Join(Environment.NewLine, rovers.Select(rover => rover.ToString())));
    }
  }
}
