using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;

namespace Notifications.Web
{
    public class CurrentPrincipalUserProvider : IUserProvider
    {
        public string GetId(HubCallerContext context)
        {
            return context.User.Identity.Name;
        }
    }
}