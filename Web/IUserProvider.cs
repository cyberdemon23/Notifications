using SignalR.Hubs;

namespace Notifications.Web
{
    public interface IUserProvider
    {
        string GetId(dynamic caller, HubCallerContext context);
    }
}
