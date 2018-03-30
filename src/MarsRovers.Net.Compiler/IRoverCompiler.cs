using System.Collections.Generic;

namespace MarsRovers.Net.Compiler
{
  /// <summary>
  /// Compiles the result of an <see cref="IRoverParser"/>
  /// </summary>
  public interface IRoverCompiler
  {
    void DeclareCommands(IEnumerable<char> commands);
    void DeclareMap(double x, double y);
    void DeclareRover(double x, double y, string heading);
    void Finish();
  }
}