using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Jeremy.Service.Configuration
{
  public class SettingsSection : ConfigurationSection
  {
    [ConfigurationProperty("username")]
    public string Username {
      get { return (string)base["username"]; }
      set { base["username"] = value; }
    }

    [ConfigurationProperty("password")]
    public string Password {
      get { return (string)base["password"]; }
      set { base["password"] = value; }
    }

    [ConfigurationProperty("queueSize", DefaultValue = 1024)]
    public int QueueSize {
      get { return (int)base["queueSize"]; }
      set { base["queueSize"] = value; }
    }
  }
}
