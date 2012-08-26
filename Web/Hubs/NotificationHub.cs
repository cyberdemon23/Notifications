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
    public class NotificationHub : Hub, IConnected
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationHub(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        /// <summary>
        /// Currently, the SignalR code for the JTokenValue is using a hard coded JsonSerializer,
        /// //and now using the one with the settings that I injected, so for the time being
        /// //I'm just going to take in a string and then turn it into the objectId
        /// </summary>
        /// <param name="messageId"></param>
        public void Remove(string messageId)
        {
            var objectId = new ObjectId(messageId);
            _notificationRepository.Remove(objectId);
        }

        public Task Connect()
        {
            return RegisterAndSendMessages();
        }

        public Task Reconnect(IEnumerable<string> groups)
        {
            return RegisterAndSendMessages();
        }

        private Task RegisterAndSendMessages()
        {
            return Task.Factory.StartNew(() =>
            {
                var userName = Context.User.Identity.Name;
                Groups.Add(Context.ConnectionId, userName); 

                var notifications = _notificationRepository.Get(userName);

                if (notifications == null || notifications.Count() == 0)
                {
                    return;
                }

                Clients[userName].notify(notifications);
                return;
            });
        }
    }
}