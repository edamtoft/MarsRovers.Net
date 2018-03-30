using MarsRovers.Net.Compiler.Exceptions;
using MarsRovers.Net.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace MarsRovers.Net.Compiler
{
  /// <summary>
  /// Parses rover syntax and passes the result along to an <see cref="IRoverCompiler"/>
  /// </summary>
  public sealed class RoverParser : IRoverParser
  {
    private ParserState State { get; set; } = ParserState.Initial;
    private int LineNumber { get; set; }
    private string Line { get; set; }
    public IRoverCompiler Compiler { get; }

    private readonly string[] CommentTokens = new[] { "#", "//" };

    public RoverParser(IRoverCompiler compiler)
    {
      Compiler = compiler;
    }

    public void Parse(TextReader input)
    {
      while (!string.IsNullOrEmpty(Line = input.ReadLine()?.Split(CommentTokens, 2, StringSplitOptions.None)[0].Trim()))
      {
        ParseLine();
        LineNumber++;
      }

      Compiler.Finish();
    }

    public bool TryParse(TextReader input, out string error)
    {
      try
      {
        Parse(input);
        error = null;
        return true;
      }
      catch (Exception err)
      {
        error = err.Message;
        return false;
      }
    }

    private void ParseLine()
    {
      switch (State)
      {
        case ParserState.Initial:
          ParseMapDeclaration();
          break;
        case ParserState.MapDeclared:
          ParseRoverDeclaration();
          break;
        case ParserState.RoverDeclared:
          ParseRoverCommands();
          break;
        case ParserState.CommandsDeclared:
          ParseRoverDeclaration();
          break;
      }
    }

    private void ParseRoverCommands()
    {
      Compiler.DeclareCommands(Line);

      State = ParserState.CommandsDeclared;
    }

    private void ParseRoverDeclaration()
    {
      var match = Regex.Match(Line, @"^(\d+) (\d+) (\w+)$", RegexOptions.IgnoreCase);

      if (!match.Success)
      {
        throw new ParserException($"Expected Rover Definition (I.E. '1 3 N'), but got '{Line}'", LineNumber);
      }

      var x = ParseNumber(match.Groups[1].Value);
      var y = ParseNumber(match.Groups[2].Value);
      var heading = match.Groups[3].Value;

      Compiler.DeclareRover(x, y, heading);

      State = ParserState.RoverDeclared;
    }

    private void ParseMapDeclaration()
    {
      var match = Regex.Match(Line, @"^(\d+) (\d+)$", RegexOptions.IgnoreCase);

      if (!match.Success)
      {
        throw new ParserException($"Expected map declaration (I.E. '5 5') but got '{Line}'", LineNumber);
      }

      var x = ParseNumber(match.Groups[1].Value);
      var y = ParseNumber(match.Groups[2].Value);

      Compiler.DeclareMap(x, y);

      State = ParserState.MapDeclared;
    }

    private double ParseNumber(string number)
    {
      if (!double.TryParse(number, out var i))
      {
        throw new ParserException($"Expected number, but got '{number}'", LineNumber);
      }

      return i;
    }
  }
}
