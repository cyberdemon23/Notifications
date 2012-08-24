using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SignalR;
using Newtonsoft.Json;
using Notifications.Web.Models;
using System.Web.Mvc;

namespace Notifications.Web.Connections
{
    public class MyConnection : PersistentConnection
    {
        private readonly IUserConnectionRepository _userConnectionRepository;
        private readonly INotificationRepository _notificationRepository;

        public MyConnection(IUserConnectionRepository userConnectionRepository, INotificationRepository notificationRepository)
        {
            _userConnectionRepository = userConnectionRepository;
            _notificationRepository = notificationRepository;
        }

        protected override Task OnConnectedAsync(IRequest request, string connectionId)
        {
            var userName = request.User.Identity.Name;
            _userConnectionRepository.Register(userName, connectionId);
            var messages = _notificationRepository.Get(userName);

            if (messages == null || messages.Count() == 0)
            {
                return base.OnConnectedAsync(request, connectionId);
            }

            return Connection.Send(connectionId, messages);
        }

        protected override Task OnDisconnectAsync(string connectionId)
        {
            _userConnectionRepository.Deregister(connectionId);
           return base.OnDisconnectAsync(connectionId);
        }
    }
}