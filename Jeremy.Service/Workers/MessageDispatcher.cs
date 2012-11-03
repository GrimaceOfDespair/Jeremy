using System;
using System.Linq;
using Jeremy.Service.Configuration;
using Jeremy.Service.Live;
using Jeremy.Service.Messaging;
using MSNPSharp;

namespace Jeremy.Service.Workers
{
  public class MessageDispatcher : Worker
  {
    private readonly MessageQueue _messageQueue;
    private readonly MessengerService _messengerService;

    public MessageDispatcher(MessageDispatcherSection configuration, MessageQueue messageQueue, MessengerService messengerService) : base(configuration)
    {
      _messageQueue = messageQueue;
      _messengerService = messengerService;
    }

    public override NextAction Action()
    {
      var message = _messageQueue.Peek();

      // Bail out if there's no message, the messenger service is not connected, or there are no available contacts
      if (message == null || _messengerService.CanSend == false) return NextAction.CanWait;

      _messageQueue.Dequeue();
      _messengerService.Send(message);

      return NextAction.ShouldRunImmediately;
    }
  }
}
