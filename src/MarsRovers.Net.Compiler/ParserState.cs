using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRovers.Net.Compiler
{
  /// <summary>
  /// Represents the current state of a rover parser
  /// </summary>
  internal enum ParserState
  {
    Initial = 0,
    MapDeclared,
    RoverDeclared,
    CommandsDeclared,
  }
}
