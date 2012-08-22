using Notifications.Web.Connections;
using SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notifications.Web
{
    public interface INotificationSender
    {
        void Send(string userName, string message);
    }

    public class NotificationSender : INotificationSender
    {
        private readonly IConnectionManager _connectionManager;
        private readonly IUserConnectionRepository _userConnectionRepository;

        public NotificationSender(IConnectionManager connectionManager, IUserConnectionRepository userConnectionRepository)
        {
            _connectionManager = connectionManager;
            _userConnectionRepository = userConnectionRepository;
        }

        public void Send(string userName, string message)
        {
            var connectionId = _userConnectionRepository.GetConnectionId(userName);
            var context = _connectionManager.GetConnectionContext<MyConnection>();
            context.Connection.Send(connectionId, message);
        }
    }
}