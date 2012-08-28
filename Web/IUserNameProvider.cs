using SignalR.Hubs;

namespace Notifications.Web
{
    public interface IUserNameProvider
    {
        string Get(HubCallerContext context);
    }
}
