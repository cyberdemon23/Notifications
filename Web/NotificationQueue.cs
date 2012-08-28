using Notifications.Web.Hubs;
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
        private readonly INotificationRepository _notificationRepository;

        public NotificationQueue(IConnectionManager connectionManager, INotificationRepository notificationRepository)
        {
            _connectionManager = connectionManager;
            _notificationRepository = notificationRepository;
        }

        public void Enqueue(Notification notification)
        {
            var context = _connectionManager.GetHubContext<NotificationHub>();
            _notificationRepository.Insert(notification);

            context.Clients[notification.UserId].notify(new List<Notification>() { notification });
        }
    }
}