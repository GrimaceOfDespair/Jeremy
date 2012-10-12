using System.Configuration;

namespace Jeremy.Service.Configuration
{
  public class JeremyConfigurationGroup : ConfigurationSectionGroup
  {
    [ConfigurationProperty("settings")]
    public SettingsSection Settings
    {
      get { return (SettingsSection)Sections["settings"]; }
    }

    [ConfigurationProperty("messageDispatcher")]
    public MessageDispatcherSection MessageDispatcher
    {
      get { return (MessageDispatcherSection)Sections["messageDispatcher"]; }
    }

    [ConfigurationProperty("messageListener")]
    public MessageListenerSection MessageListener
    {
      get { return (MessageListenerSection)Sections["messageListener"]; }
    }

    [ConfigurationProperty("udpListener")]
    public UdpListenerSection UdpListener
    {
      get { return (UdpListenerSection)Sections["udpListener"]; }
    }

    public ConfigurationSection[] GetSections()
    {
      return new ConfigurationSection[]
               {
                 Settings,
                 MessageDispatcher,
                 MessageListener,
                 UdpListener
               };

    }
  }
}
