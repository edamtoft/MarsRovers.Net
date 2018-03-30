using MarsRovers.Net.Compiler;
using System;
using System.IO;
using System.Linq;

namespace MarsRovers.Net.App
{
  /// <summary>
  /// Entry Point
  /// </summary>
  static class Program
  { 
    static int Main(string[] args)
    {
      var app = new CliApp(args);
      return app.TryRun() ? 0 : 1;
    }
  }
}
