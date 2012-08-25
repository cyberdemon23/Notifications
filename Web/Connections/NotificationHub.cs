using MongoDB.Bson;
using Newtonsoft.Json;
using SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Notifications.Web.Connections
{
    public class NotificationHub : Hub, IDisconnect, IConnected
    {
        private readonly IUserConnectionRepository _userConnectionRepository;
        private readonly INotificationRepository _notificationRepository;

        public NotificationHub(IUserConnectionRepository userConnectionRepository, INotificationRepository notificationRepository)
        {
            _userConnectionRepository = userConnectionRepository;
            _notificationRepository = notificationRepository;
        }

        /// <summary>
        /// Currently, the SignalR code for the JTokenValue is using a hard coded JsonSerializer,
        /// //and now using the one with the settings that I injected, so for the time being
        /// //I'm just going to take in a string and then turn it into the objectId
        /// </summary>
        /// <param name="messageIdString"></param>
        public void Remove(string messageId)
        {
            var objectId = new ObjectId(messageId);
            _notificationRepository.Remove(objectId);
        }

        public Task Disconnect()
        {
            return Task.Factory.StartNew(() =>
            {
                _userConnectionRepository.Deregister(Context.ConnectionId);
            });
        }

        public Task Connect()
        {
            return SendOutstandingMessages();
        }

        public Task Reconnect(IEnumerable<string> groups)
        {
            return SendOutstandingMessages();
        }

        private Task SendOutstandingMessages()
        {
            return Task.Factory.StartNew(() =>
            {
                var userName = Context.User.Identity.Name;
                _userConnectionRepository.Register(userName, Context.ConnectionId);
                var notifications = _notificationRepository.Get(userName);

                if (notifications == null || notifications.Count() == 0)
                {
                    return;
                }

                Clients[Context.ConnectionId].notify(notifications);
                return;
            });
        }
    }
}