using System;
using System.Threading;

namespace Jeremy.Service.Workers
{
  public interface IWorker
  {
    void Run(ManualResetEvent stopEvent);
  }
}
