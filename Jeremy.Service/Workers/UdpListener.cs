using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using Jeremy.Service.Configuration;
using Jeremy.Service.Messaging;
using log4net;

namespace Jeremy.Service.Workers
{
  public class UdpListener : Worker
  {
    private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    private readonly MessageQueue _messageQueue;
    private readonly MessageFactory _messageFactory;
    private readonly UdpClient _udpClient;

    public UdpListener(UdpListenerSection configuration, MessageQueue messageQueue, MessageFactory messageFactory) : base(configuration)
    {
      _messageQueue = messageQueue;
      _messageFactory = messageFactory;
      _udpClient = new UdpClient(configuration.Port);
    }

    public override NextAction Action()
    {
      var sender = new IPEndPoint(IPAddress.Any, 0);
      var buffer = _udpClient.Receive(ref sender);
      var line = System.Text.Encoding.Default.GetString(buffer);

      if (string.IsNullOrEmpty(line)) return NextAction.CanWait;

      if (Log.IsDebugEnabled)
      {
        Log.Debug("Incoming log entry: " + line);
      }

      var message = _messageFactory.Create(line);

      _messageQueue.Enqueue(message);

      return NextAction.ShouldRunImmediately;
    }
  }
}
