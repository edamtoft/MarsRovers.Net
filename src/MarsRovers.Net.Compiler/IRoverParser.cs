using System;
using System.IO;
using System.Linq.Expressions;

namespace MarsRovers.Net.Compiler
{
  /// <summary>
  /// Parses tokens and validates flow/syntax and passes the result to an <see cref="IRoverCompiler"/>.
  /// Note that the parser does not validate correctness, but simply syntax/flow.
  /// 
  /// I.E. the compiler will tell if a given command is valid. The parser will just validate that it's structurally/syntactically correct.
  /// </summary>
  public interface IRoverParser
  {
    void Parse(TextReader input);
    bool TryParse(TextReader input, out string error);
  }
}