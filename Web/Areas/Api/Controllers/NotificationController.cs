using Notifications.Web.Areas.Api.Models;
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
        // POST api/notification
        public async Task Post(Notification entity)
        {
            var connection = new Connection("http://localhost:1174/echo");
            await connection.Start();
            await connection.Send(entity.Message);
            connection.Stop();
        }
    }
}
