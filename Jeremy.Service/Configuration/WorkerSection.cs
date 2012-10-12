using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Jeremy.Service.Configuration
{
  public class WorkerSection : ConfigurationSection
  {
    [ConfigurationProperty("interval", DefaultValue = 5000)]
    public int Interval
    {
      get { return (int)base["interval"]; }
      set { base["interval"] = value; }
    }
  }
}
