using MarsRovers.Net.Compiler.Exceptions;
using MarsRovers.Net.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MarsRovers.Net.Compiler
{
  /// <summary>
  /// Compiles rover syntax to an delegate which will execute all commands against a <see cref="Rover"/>.
  /// </summary>
  public sealed class RoverCompiler : IRoverCompiler
  {
    private List<ParameterExpression> Rovers { get; } = new List<ParameterExpression>();
    private ParameterExpression MapVariable { get; } = Expression.Variable(typeof(Map), "map");
    private List<Expression> Commands { get; } = new List<Expression>();
    private bool MapInitialized { get; set; }
    public Func<IRover[]> Mission { get; private set; }

    public void DeclareMap(double x, double y)
    {
      if (MapInitialized)
      {
        throw new CompilerException("Map already initialized");
      }

      var mapConstructor = typeof(Map).GetConstructor(new[] { typeof(double), typeof(double) });

      Commands.Add(Expression.Assign(MapVariable, Expression.New(mapConstructor, Expression.Constant(x), Expression.Constant(y))));

      MapInitialized = true;
    }

    public void DeclareRover(double x, double y, string heading)
    {
      var rover = Expression.Variable(typeof(Rover), "rover" + Rovers.Count);

      Rovers.Add(rover);

      var direction = GetDirection(heading);
      var xValue = Expression.Constant(x);
      var yValue = Expression.Constant(y);

      if (MapInitialized)
      {
        var ctor = typeof(TrackedRover).GetConstructor(new[] { typeof(double), typeof(double), typeof(Direction), typeof(IMap) });
        Commands.Add(Expression.Assign(rover, Expression.New(ctor, xValue, yValue, direction, MapVariable)));
      }
      else
      {
        var ctor = typeof(Rover).GetConstructor(new[] { typeof(double), typeof(double), typeof(Direction) });
        Commands.Add(Expression.Assign(rover, Expression.New(ctor, xValue, yValue, direction)));
      }
    }

    private Expression GetDirection(string heading)
    {
      switch (heading?.ToUpper())
      {
        case "N":
        case "NORTH":
          return Expression.Property(null, typeof(Direction), nameof(Direction.North));
        case "E":
        case "EAST":
          return Expression.Property(null, typeof(Direction), nameof(Direction.East));
        case "S":
        case "SOUTH":
          return Expression.Property(null, typeof(Direction), nameof(Direction.South));
        case "W":
        case "WEST":
          return Expression.Property(null, typeof(Direction), nameof(Direction.West));
        default:
          throw new CompilerException($"Unexpected direction '{heading}'");
      }
    }

    public void DeclareCommands(IEnumerable<char> commands)
    {
      var rover = Rovers.LastOrDefault();

      if (rover == null)
      {
        throw new CompilerException("Recieved commands, but no rover is declared");
      }

      foreach (var command in commands)
      {
        switch (char.ToUpper(command))
        {
          case 'L':
            Commands.Add(Expression.Call(rover, nameof(Rover.TurnLeft), Type.EmptyTypes));
            break;
          case 'R':
            Commands.Add(Expression.Call(rover, nameof(Rover.TurnRight), Type.EmptyTypes));
            break;
          case 'M':
            Commands.Add(Expression.Call(rover, nameof(Rover.MoveForward), Type.EmptyTypes));
            break;
          default:
            throw new CompilerException($"Unexpected Command '{command}'");
        }
      }
    }

    public void Finish()
    {
      Commands.Add(Expression.NewArrayInit(typeof(Rover), Rovers));
      var variables = new List<ParameterExpression>(Rovers)
      {
        MapVariable
      };
      var block = Expression.Block(typeof(Rover[]), variables, Commands);
      Mission = Expression.Lambda<Func<Rover[]>>(block).Compile();
    }
  }
}
