using Newtonsoft.Json;
using Notifications.Web.Models;
using SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Notifications.Web.Areas.Api.Controllers
{
    public class NotificationController : ApiController
    {
        private INotificationQueue _notificationQueue;

        public NotificationController(INotificationQueue notificationSender)
        {
            _notificationQueue = notificationSender;
        }

        // POST api/notification
        public async Task Post(Notification entity)
        {
            _notificationQueue.Enqueue(entity);    
        }
    }
}
