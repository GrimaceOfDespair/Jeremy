using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Jeremy.Service.Configuration;
using Jeremy.Service.Messaging;
using Jeremy.Service.Workers;
using log4net;

namespace Jeremy.Service
{
  public partial class CommunicationService : ServiceBase
  {
    public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    protected readonly Dictionary<ManualResetEvent, IWorker> Runnables = new Dictionary<ManualResetEvent, IWorker>();
    public ManualResetEvent StopEvent { get; set; }
    protected int Timeout { get; set; }

    public CommunicationService(IEnumerable<IWorker> workers)
    {
      InitializeComponent();

      foreach (var worker in workers)
      {
        Runnables[new ManualResetEvent(false)] = worker;
      }
    }

    protected override void OnStart(string[] args)
    {
      Run();
    }

    public bool IsStopping
    {
      get { return StopEvent.WaitOne(0); }
    }

    protected override void OnStop()
    {
      if (Log.IsDebugEnabled)
      {
        Log.Debug("Stopping CommunicationService");
      }

      StopEvent.Set();

      WaitHandle.WaitAll(Runnables.Keys.Cast<WaitHandle>().ToArray(), Timeout);
    }

    public void Run()
    {
      if (Log.IsDebugEnabled)
      {
        Log.Debug("Running CommunicationService");
      }

      foreach (var runnable in Runnables)
      {
        var run = runnable.Value;
        var @event = runnable.Key;

        ThreadPool.QueueUserWorkItem(
          state =>
            {
              try
              {
                if (Log.IsDebugEnabled)
                {
                  Log.Debug("Starting " + run.GetType().Name);
                }

                run.Run(StopEvent);
              }
              finally
              {
                if (Log.IsDebugEnabled)
                {
                  Log.Debug(run.GetType().Name + " is done");
                }

                @event.Set();
              }
            });
      }
    }
  }
}
