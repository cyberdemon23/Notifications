using Notifications.Web.Models;
using System;
using System.Web.Http;

namespace Notifications.Web.Areas.Api.Controllers
{
    public class NotificationController : ApiController
    {
        private readonly INotificationQueue _notificationQueue;

        public NotificationController(INotificationQueue notificationSender)
        {
            _notificationQueue = notificationSender;
        }

        // POST api/notification
        public void Post(Notification entity)
        {
            entity.CreatedDateTime = DateTime.Now;
            _notificationQueue.Enqueue(entity);    
        }
    }
}
