using SignalR.Hubs;

namespace Notifications.Web
{
    public class CurrentPrincipalUserProvider : IUserProvider
    {
        public string GetId(dynamic caller, HubCallerContext context)
        {
            var userId = caller.userId;
            return context.User.Identity.Name;
        }
    }
}