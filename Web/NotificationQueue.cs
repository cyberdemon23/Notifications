using Notifications.Web.Connections;
using Notifications.Web.Models;
using SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notifications.Web
{
    public interface INotificationQueue
    {
        void Enqueue(Notification notification);
    }

    public class NotificationQueue : INotificationQueue
    {
        private readonly IConnectionManager _connectionManager;
        private readonly IUserConnectionRepository _userConnectionRepository;
        private readonly INotificationRepository _notificationRepository;

        public NotificationQueue(IConnectionManager connectionManager, IUserConnectionRepository userConnectionRepository, INotificationRepository notificationRepository)
        {
            _connectionManager = connectionManager;
            _userConnectionRepository = userConnectionRepository;
            _notificationRepository = notificationRepository;
        }

        public void Enqueue(Notification notification)
        {
            var connectionIds = _userConnectionRepository.GetConnectionIds(notification.UserName);
            var context = _connectionManager.GetHubContext<NotificationHub>();
            _notificationRepository.Insert(notification);

            foreach (var connectionId in connectionIds)
            {
                context.Clients[connectionId].notify(new List<Notification>() { notification });
            }
        }
    }
}