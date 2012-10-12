using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jeremy.Service.Messaging
{
  public class MessageFactory
  {
    private readonly MessageQueue _messageQueue;

    public MessageFactory(MessageQueue messageQueue)
    {
      _messageQueue = messageQueue;
    }

    public Message Create(string line)
    {
      var message = new Message(line);

      return message;
    }
  }
}
