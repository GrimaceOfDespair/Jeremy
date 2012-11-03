using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Jeremy.Service.Configuration;
using log4net;

namespace Jeremy.Service.Workers
{
  public abstract class Worker : IWorker
  {
    private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    private readonly int _interval;

    protected Worker(WorkerSection configuration)
    {
      if (Log.IsDebugEnabled)
      {
        Log.Debug("Creating worker " + GetType().Name + " which will run at an interval of " + configuration.Interval + " milliseconds");
      }

      _interval = configuration.Interval;
    }

    public void Run(ManualResetEvent stopEvent)
    {
      while (stopEvent.WaitOne(0) == false)
      {
        if (Log.IsDebugEnabled)
        {
          Log.Debug("Worker " + GetType().Name + " runs");
        }

        bool canWait;
        try
        {
          canWait = Action() == NextAction.CanWait;
        }
        catch (Exception e)
        {
          if (Log.IsErrorEnabled)
          {
            Log.Error("Error while running", e);
          }
          canWait = true;
        }

        if (canWait)
        {
          stopEvent.WaitOne(_interval);
        }
      }
    }

    // Returns whether action wants to run again rightaway
    public abstract NextAction Action();
  }
}
