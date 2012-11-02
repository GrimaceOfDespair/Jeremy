using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using log4net;

namespace Jeremy.Console
{
  class Program
  {
    public static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    static void Main(string[] args)
    {
      System.Console.WriteLine("Type anything followed by carriage return to send it to log4net");

      string line;
      while (string.IsNullOrEmpty(line = System.Console.ReadLine()) == false)
      {
        Log.Debug(line);
      }
    }
  }
}
