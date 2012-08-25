using MongoDB.Bson;
using SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Notifications.Web.Connections
{
    public class NotificationsHub : Hub, IDisconnect, IConnected
    {
        private readonly IUserConnectionRepository _userConnectionRepository;
        private readonly INotificationRepository _notificationRepository;

        public NotificationsHub(IUserConnectionRepository userConnectionRepository, INotificationRepository notificationRepository)
        {
            _userConnectionRepository = userConnectionRepository;
            _notificationRepository = notificationRepository;
        }

        public void Delete(ObjectId messageId)
        {

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