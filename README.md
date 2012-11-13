Jeremy
======

.NET Windows Service to broadcast log4net message to Windows Live contacts.

Example configuration for the service:

    <configuration>
      <configSections>
        <sectionGroup name="jeremyService" type="Jeremy.Service.Configuration.JeremyConfigurationGroup, Jeremy.Service">
          <section name="settings" type="Jeremy.Service.Configuration.SettingsSection, Jeremy.Service"/>
          <section name="udpListener" type="Jeremy.Service.Configuration.UdpListenerSection, Jeremy.Service"/>
          <section name="messageDispatcher" type="Jeremy.Service.Configuration.MessageDispatcherSection, Jeremy.Service"/>
          <section name="messageListener" type="Jeremy.Service.Configuration.MessageListenerSection, Jeremy.Service"/>
        </sectionGroup>
      </configSections>
      <jeremyService>
        <settings username="john@example.com" password="********" queueSize="10"/>
        <udpListener port="8081"/>
        <messageDispatcher/>
        <messageListener/>
      </jeremyService>
    </configuration>



Example configuration for the logging application:

    <configuration>
      <configSections>
        <section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net"/>
      </configSections>
      <log4net debug="true">
        <appender name="UdpAppender" type="log4net.Appender.UdpAppender">
          <threshold value="ERROR" />
          <remoteAddress value="127.0.0.1" />
          <remotePort value="8081" />
          <layout type="log4net.Layout.PatternLayout">
            <conversionPattern value="%message%newline" />
          </layout>
        </appender>
        <root>
          <appender-ref ref="UdpAppender"/>
        </root>
      </log4net>
    </configuration>
