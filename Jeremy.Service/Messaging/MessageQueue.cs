using System.Collections.Generic;
using System.Linq;
using Jeremy.Service.Configuration;

namespace Jeremy.Service.Messaging
{
  public class MessageQueue
  {
    private readonly Queue<Message> _innerQueue = new Queue<Message>();

    private readonly int _queueSize;

    public MessageQueue(SettingsSection settings)
    {
      _queueSize = settings.QueueSize;
    }

    public void Enqueue(Message message)
    {
      lock (_innerQueue)
      {
        _innerQueue.Enqueue(message);
        while (_innerQueue.Count > _queueSize) _innerQueue.Dequeue();
      }
    }

    public Message Dequeue()
    {
      lock (_innerQueue)
      {
        return _innerQueue.Any() ? _innerQueue.Dequeue() : null;
      }
    }
  }

}