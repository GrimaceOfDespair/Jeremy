namespace Jeremy.Service.Messaging
{
  public class Message
  {
    private readonly string _text;
    private readonly string _recipient;

    public Message(string text, string recipient)
    {
      _text = text;
      _recipient = recipient;
    }

    public Message(string text) : this(text, "")
    {
    }

    public string Recipient
    {
      get { return _recipient; }
    }

    public string Text
    {
      get { return _text; }
    }

    public bool IsBroadCast
    {
      get { return string.IsNullOrEmpty(_recipient); }
    }
  }
}
