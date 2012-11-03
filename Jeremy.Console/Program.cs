using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net;
using log4net.Core;

namespace Jeremy.Console
{
  class Program
  {
    public static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    public static Level Level;

    static void Main(string[] args)
    {
      System.Console.WriteLine("Type anything followed by carriage return to send it to log4net. Type !level=ERROR or !level=DEBUG to toggle mode. Enter an empty line to close.");

      string line;
      while (string.IsNullOrEmpty(line = System.Console.ReadLine()) == false)
      {
        if (line.StartsWith("!"))
        {
          ProcessCommand(line.Substring(1));
        }
        else
        {
          switch (Level.Name)
          {
            case "DEBUG":
              Log.Debug(line);
              break;
            case "INFO":
              Log.Info(line);
              break;
            case "WARN":
              Log.Warn(line);
              break;
            case "ERROR":
              Log.Error(line);
              break;
            case "FATAL":
              Log.Fatal(line);
              break;
          }
          
        }
      }
    }

    private static void ProcessCommand(string substring)
    {
      var commandParts = substring.Split(new[] {'='}, 2);

      if (commandParts.Length == 2)
      {
        var variable = commandParts[0];
        var value = commandParts[1];
        switch (variable)
        {
          case "level":
            SetMode(value);
            break;
        }
      }
    }

    private static void SetMode(string levelValue)
    {
      var levels = new[]
                     {
                       Level.Debug,
                       Level.Info,
                       Level.Warn,
                       Level.Error,
                       Level.Fatal
                     };

      foreach (var level in levels.Where(level => level.Name == levelValue))
      {
        System.Console.WriteLine("Setting level to " + level.Name);
        Level = level;
        return;
      }
    }
  }
}
