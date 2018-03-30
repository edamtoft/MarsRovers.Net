# Mars Rovers Compiler

## Background

This is meant to be a rather useless, but somewhat interesting take on a basic problem commonly used as a simple code test. The general background is that you must parse a simple syntax where you first define the size of the 'plateau' on which the rovers will rove, then define rovers followed by a set of commands to send to the rover. A sample input would look like:

```
5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
```
The above input declares a map size of 5x5, declares a rover at position (1,2) facing north, and then sends it instructions formatted as M = move forward, L = turn left, and R = turn right. It then declares a second rover at (3,3) facing east and sends instructions to that rover.

The expected output is the position and heading of each rover

```
1 3 N
5 1 E
```

## About this implementation

This may seem like a simple problem that could be solved in a few lines of code, and it is! However, because it's so simple, this code test provides a good "proving ground" for design patterns, and code samples that can be used without getting caught up too much in the problem domain. This deliberately meant to be an overkill solution for the actual problem.

I decided to take the viewpoint that the syntax for defining and providing instructions for rovers is basically a very simple programming language, and rather than simply interpret it step-by-step, it instead could be "compiled" to an executable. This implementation uses Linq expression trees to compile a lambda which will actually create and move the rovers. The benefit of this is that it provides the ability  to take the same syntax tree and re-interpret it into another language such as SQL, javascript, etc. 

### Assumptions

Though the problem does not define what to do when rovers move off the plateau or into each-other, I created an extension of Rover which allows for it to report it's position on the map and have a status flag set in the event of a collision. If a rover collides or moves off the map, it will ignore any further instructions

### Domain Types

There are a number of immutable structs defined. Position (a single point on a 2d point), Direction (degrees from north), and Vector (a direction + a distance). These types have all the operator overloads to allow the actual "business" logic to be incredibly simple.

```csharp
class Rover
{
  public Direction Heading { get; private set; }
  public Position Location { get; private set; }

  public void TurnLeft()
  {
    Heading = Heading.Left;
  }

  public void TurnRight()
  {
    Heading = Heading.Right;
  }

  public void MoveForward()
  {
    Location += (Heading * 1);
  }
}
```

All of the core logic of how the rover moves exists in the above code. Heading has a .Right and .Left property which provide the relative directions, and Location += (Heading * 1) easily shows that you're moving 1 unit in a given direction.

At this point, if we could just hard-code the input instead of having to parse the rover syntax, we could just write

```csharp
var rover = new Rover(1,2,Direction.North);
rover.TurnLeft();
rover.MoveForward();
rover.TurnLeft();
rover.MoveForward();
// ... etc.
```

### Parsing

The parsing is separated into a compiler and a parser. The parser just reads the syntax of the rover language and sends the parsed tokens a compiler which in turn builds an expression tree. The expression tree is essentially a representation of the above code, which is compiled to a lambda which returns an array of the rovers at the end.

### Command Line

The command line interface is meant to provide a simple interface to parsing and executing the rover syntax and handling the I/O. The default behavior is to take the rover input from stdin and write the response to stdout (or stderr if something goes wrong). There are a couple of command line flags which will enable additional functionality:

* --help -h : Show a human-readable explanation of the expected input and output
* --file <filename> : Read from a file instead of stdin
* --keepopen : Keep the console open after executing the commands (useful if running from visual studio)
* --retry : Allow the user to modify a line and continue parsing if an unexpected input is encountered