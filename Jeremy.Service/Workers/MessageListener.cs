using System;
using System.Threading;
using Jeremy.Service.Configuration;

namespace Jeremy.Service.Workers
{
  public class MessageListener : Worker
  {
    public MessageListener(MessageListenerSection configuration) : base(configuration)
    {
      
    }

    public override NextAction Action()
    {
      return NextAction.CanWait;
    }
  }
}
