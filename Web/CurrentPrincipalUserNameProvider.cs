using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;

namespace Notifications.Web
{
    public class CurrentPrincipalUserNameProvider : IUserNameProvider
    {
        public string Get(HubCallerContext context)
        {
            return context.User.Identity.Name;
        }
    }
}