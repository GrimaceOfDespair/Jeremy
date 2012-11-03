using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Jeremy.Service.Configuration;
using Jeremy.Service.Messaging;
using MSNPSharp;
using log4net;

namespace Jeremy.Service.Live
{
  public class MessengerService
  {
    private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    private readonly Messenger _messenger;

    public MessengerService(SettingsSection settingsSection)
    {
      if (Log.IsDebugEnabled)
      {
        Log.Debug("Creating MessengerService");
      }

      _messenger = new Messenger
                     {
                       Credentials =
                         {
                           Account = settingsSection.Username,
                           Password = settingsSection.Password
                         }
                     };

      _messenger.ConnectingException += OnMessengerException;
      _messenger.ConnectionEstablished += OnMessengerConnectionEstablished;
      _messenger.ConnectionClosed += OnMessengerConnectionClosed;

      _messenger.Nameserver.ExceptionOccurred += OnMessengerException;
      _messenger.Nameserver.AuthenticationError += OnMessengerException;
      _messenger.Nameserver.ServerErrorReceived += OnNameserverServerErrorReceived;
      _messenger.Nameserver.ContactOnline += OnNameServerContactOnline;
      _messenger.Nameserver.ContactOffline += OnNameServerContactOffline;
      _messenger.Nameserver.SignedIn += OnNameServerSignedIn;
      _messenger.Nameserver.SignedOff += OnNameServerSignedOff;

      _messenger.ContactService.SynchronizationCompleted += OnContactServiceSynchronizationCompleted;

      _messenger.Connect();
    }

    private void OnNameServerSignedIn(object sender, EventArgs e)
    {
      if (Log.IsErrorEnabled)
      {
        Log.Error("Signed in");
      }

      _messenger.Owner.Status = PresenceStatus.Online;
    }

    private void OnNameServerSignedOff(object sender, EventArgs e)
    {
      if (Log.IsErrorEnabled)
      {
        Log.Error("Signed off");
      }
    }

    private void OnNameServerContactOnline(object sender, ContactStatusChangedEventArgs e)
    {
      if (Log.IsErrorEnabled)
      {
        Log.Error("Contact online: " + e.Contact.Name);
      }
    }

    private void OnNameServerContactOffline(object sender, ContactStatusChangedEventArgs e)
    {
      if (Log.IsErrorEnabled)
      {
        Log.Error("Contact offline: " + e.Contact.Name);
      }
    }

    private void OnNameserverServerErrorReceived(object sender, MSNErrorEventArgs msnErrorEventArgs)
    {
        if (Log.IsErrorEnabled)
        {
          Log.Error("MSN server error [" + msnErrorEventArgs.MSNError + "] - " + msnErrorEventArgs.Description);
        }
    }

    private void OnContactServiceSynchronizationCompleted(object sender, EventArgs eventArgs)
    {
      if (Log.IsDebugEnabled)
      {
        Log.Debug("Address book was loaded");
      }
    }

    private void OnMessengerConnectionClosed(object sender, EventArgs eventArgs)
    {
      if (Log.IsDebugEnabled)
      {
        Log.Debug("Connection closed");
      }
    }

    private void OnMessengerConnectionEstablished(object sender, EventArgs eventArgs)
    {
      if (Log.IsDebugEnabled)
      {
        Log.Debug("Connection established");
      }
    }

    private void OnMessengerException(object sender, ExceptionEventArgs exceptionEventArgs)
    {
      if (Log.IsErrorEnabled)
      {
        Log.Error("Messenger exception", exceptionEventArgs.Exception);
      }
    }

    public bool CanSend
    {
      get { return _messenger.Connected && _messenger.ContactService.AddressBookSynchronized; }
    }

    public void Send(Message message)
    {
      foreach (var contact in _messenger.ContactList.WindowsLive)
      {
        if (Log.IsDebugEnabled)
        {
          Log.Debug("Checking contact " + contact.Name + " for sending message");
        }

        //if (contact.Online)
        {
          if (Log.IsDebugEnabled)
          {
            Log.Debug("Sending message to " + contact.Name);
          }

          //var textMessage = new TextMessage(message.Text);
          //_messenger.MessageManager.SendTextMessage(contact, textMessage);
          _messenger.SendTextMessage(contact, message.Text);
        }
      }
    }
  }

}
