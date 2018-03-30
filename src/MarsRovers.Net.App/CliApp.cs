using MarsRovers.Net.Compiler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MarsRovers.Net.App
{
  /// <summary>
  /// Executes the command line interface
  /// </summary>
  sealed class CliApp
  {
    private readonly string[] _args;
    private readonly RoverCompiler _compiler;
    private readonly RoverParser _parser;

    public CliApp(string[] args)
    {
      _args = args;
      _compiler = new RoverCompiler();
      _parser = new RoverParser(_compiler);
    }

    public bool TryRun()
    {
      if (HasFlag("--help") || HasFlag("-h"))
      {
        Console.WriteLine(Resources.HelpText);
      }

      if (!TryRead())
      {
        return false;
      }

      Execute();

      if (HasFlag("--keepopen"))
      {
        Console.ReadKey();
      }

      return true;
    }

    private bool TryRead()
    {
      var file = ReadArg("--file");
      return file != null ? TryReadFile(file) : TryReadStdIn();
    }

    private void Execute()
    {
      var rovers = _compiler.Mission();
      foreach (var rover in rovers)
      {
        Console.WriteLine(rover.ToString());
      }
    }

    private bool TryReadFile(string path)
    {
      var file = new FileInfo(path);
      if (!file.Exists)
      {
        Console.Error.WriteLine(Resources.FileNotFound);
        return false;
      }
      using (var reader = file.OpenText())
      {
        if (_parser.TryParse(reader, out var err))
        {
          return true;
        }
        Console.Error.WriteLine(err);
        return false;
      }
    }

    private bool TryReadStdIn()
    {
      while (!_parser.TryParse(Console.In, out var err))
      {
        Console.Error.WriteLine(err);
        if (!HasFlag("--retry"))
        {
          return false;
        }
        Console.Error.WriteLine(Resources.CorrectInputToContinue);
      }
      return true;
    }

    private bool HasFlag(string flag)
    {
      return _args.Contains(flag);
    }

    private string ReadArg(string flag)
    {
      var index = Array.IndexOf(_args, flag);

      if (index != -1 && _args.Length > index)
      {
        return _args[index + 1];
      }

      return null;
    }
  }
}
