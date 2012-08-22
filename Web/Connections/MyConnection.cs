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
        private readonly IUserConnectionRepository _userConnectionRepository;

        public MyConnection()
        {
            //SignalR isn't using the DependencyResolver that I'm clearly telling it to use, so this is what I'm doing
            _userConnectionRepository = (IUserConnectionRepository)GlobalHost.DependencyResolver.GetService(typeof(IUserConnectionRepository));
        }

        public MyConnection(IUserConnectionRepository userConnectionRepository)
        {
            _userConnectionRepository = userConnectionRepository;
        }

        protected override Task OnConnectedAsync(IRequest request, string connectionId)
        {
            _userConnectionRepository.Register(request.User.Identity.Name, connectionId);
            return base.OnConnectedAsync(request, connectionId);
        }

        protected override Task OnDisconnectAsync(string connectionId)
        {
            _userConnectionRepository.Remove(connectionId);
           return base.OnDisconnectAsync(connectionId);
        }
    }
}