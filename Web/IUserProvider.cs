using SignalR.Hubs;

namespace Notifications.Web
{
    public interface IUserProvider
    {
        string GetId(HubCallerContext context);
    }
}
