using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRovers.Net.Compiler.Exceptions
{
  /// <summary>
  /// Represents an Exception raised by a <see cref="IRoverParser"/>.
  /// </summary>
  public sealed class ParserException : Exception
  {
    public int LineLumber { get; }

    public ParserException(string message, int lineNumber) : base(message)
    {
      LineLumber = lineNumber;
    }
  }
}
