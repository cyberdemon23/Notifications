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
        private INotificationSender _notificationSender;

        public NotificationController(INotificationSender notificationSender)
        {
            _notificationSender = notificationSender;
        }

        // POST api/notification
        public async Task Post(Notification entity)
        {
            _notificationSender.Send(entity.UserName, entity.Message);    
        }
    }
}
