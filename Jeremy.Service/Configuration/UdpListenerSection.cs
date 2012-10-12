using System.Configuration;

namespace Jeremy.Service.Configuration
{
  public class UdpListenerSection : WorkerSection
  {
    [ConfigurationProperty("port")]
    public int Port
    {
      get { return (int)base["port"]; }
      set { base["port"] = value; }
    }
  }
}