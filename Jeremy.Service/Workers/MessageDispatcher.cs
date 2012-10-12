using System;
using Jeremy.Service.Configuration;

namespace Jeremy.Service.Workers
{
  public class MessageDispatcher : Worker
  {
    public MessageDispatcher(MessageDispatcherSection configuration) : base(configuration)
    {
      
    }

    public override NextAction Action()
    {
      return NextAction.CanWait;
    }
  }
}
