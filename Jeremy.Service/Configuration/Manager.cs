using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Jeremy.Service.Configuration
{
  public class Manager
  {
    public static readonly JeremyConfigurationGroup Instance = GetJeremyConfiguration();

    private static JeremyConfigurationGroup GetJeremyConfiguration()
    {
      var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      return (JeremyConfigurationGroup) config.GetSectionGroup("jeremyService");
    }
  }
}
