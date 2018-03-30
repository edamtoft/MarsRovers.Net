using MarsRovers.Net.Compiler;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRovers.Net.Tests.Mocks
{
  class NoOpCompiler : IRoverCompiler
  {
    public void DeclareCommands(IEnumerable<char> commands)
    {
    }

    public void DeclareMap(double x, double y)
    {
    }

    public void DeclareRover(double x, double y, string heading)
    {
    }

    public void Finish()
    {
    }

    public void Start()
    {
    }
  }
}
