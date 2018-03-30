using MarsRovers.Net.Compiler;
using MarsRovers.Net.Compiler.Exceptions;
using MarsRovers.Net.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MarsRovers.Net.Tests.UnitTests
{
  [TestClass]
  [TestCategory(Category.UnitTest)]
  public class ParserTests
  {

    [TestMethod]
    [ExpectedException(typeof(ParserException))]
    public void FailsOnSyntacticallyIncorrectInput()
    {
      const string Input = @"BAD INPUT";
      var parser = new RoverParser(new NoOpCompiler());
      using (var reader = new StringReader(Input))
      {
        parser.Parse(reader);
      }
    }

    [TestMethod]
    public void DoesNotValidateActualCommands()
    {
      const string Input = @"5 5
1 3 Tree
ASDLKHFALSKJHASFLK
7 4 Foo
SDFLKD";
      var parser = new RoverParser(new NoOpCompiler());
      using (var reader = new StringReader(Input))
      {
        parser.Parse(reader);
      }

      Assert.IsTrue(true);
    }

    [TestMethod]
    [ExpectedException(typeof(ParserException))]
    public void ValidatesStructure()
    {
      const string Input = @"5 5
Foo Bar Baz";
      var parser = new RoverParser(new NoOpCompiler());
      using (var reader = new StringReader(Input))
      {
        parser.Parse(reader);
      }
    }
  }
}
