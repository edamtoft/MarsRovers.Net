using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRovers.Net.Compiler.Exceptions
{
  /// <summary>
  /// Represents an exception raised by an <see cref="IRoverCompiler"/>
  /// </summary>
  public sealed class CompilerException : Exception
  {
    public CompilerException(string message) : base(message)
    {
    }
  }
}
