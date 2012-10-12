namespace Jeremy.Service.Messaging
{
  public class Message
  {
    private readonly string _text;

    public Message(string text)
    {
      _text = text;
    }

    public string Text
    {
      get { return _text; }
    }
  }
}
