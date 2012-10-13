using System;
using System.Collections.Generic;
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
  static class Program
  {
    public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main()
    {
      if (Log.IsDebugEnabled)
      {
        Log.Debug("Starting and configuring application");
      }

      ConfigureContainer();

      var service = TinyIoC.TinyIoCContainer.Current.Resolve<CommunicationService>();

#if DEBUG
      if (Debugger.IsAttached)
      {
        service.Run();

        while (true) Thread.Sleep(int.MaxValue);
      }
      else
#endif
      {
        ServiceBase.Run(service);
      }
    }

    private static void ConfigureContainer()
    {
      var container = TinyIoC.TinyIoCContainer.Current;

      // Register configuration sections
      foreach (var section in Manager.Instance.GetSections())
      {
        container.Register(section.GetType(), section);
      }

      // Register workers
      container.AutoRegister(t => typeof(IWorker).IsAssignableFrom(t));

      // Register queue
      container.Register<MessageQueue>().AsSingleton();
      container.Register<MessageFactory>().AsSingleton();
      container.Register<CommunicationService>().AsSingleton();
    }
  }
}
