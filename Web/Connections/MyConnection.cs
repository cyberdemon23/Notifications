using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SignalR;
using Newtonsoft.Json;
using Notifications.Web.Models;

namespace Notifications.Web.Connections
{
    public class MyConnection : PersistentConnection
    {
        private static IUserConnectionRepository _userConnectionRepository = new UserConnectionRepository();

        protected override Task OnConnectedAsync(IRequest request, string connectionId)
        {
            _userConnectionRepository.Register(request.User.Identity.Name, connectionId);
            return base.OnConnectedAsync(request, connectionId);
        }
        protected override Task OnReceivedAsync(IRequest request, string connectionId, string data)
        {
            var notification = JsonConvert.DeserializeObject<Notification>(data);
            var recipientConnectionId = _userConnectionRepository.GetConnectionId(notification.UserName);
            return Connection.Send(recipientConnectionId, notification.Message);
        }

        protected override Task OnDisconnectAsync(string connectionId)
        {
            _userConnectionRepository.Remove(connectionId);
           return base.OnDisconnectAsync(connectionId);
        }
    }
}