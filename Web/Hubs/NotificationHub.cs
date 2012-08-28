using MongoDB.Bson;
using SignalR.Hubs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications.Web.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserProvider _userProvider;

        public NotificationHub(INotificationRepository notificationRepository, IUserProvider userProvider)
        {
            _notificationRepository = notificationRepository;
            _userProvider = userProvider;
        }

        /// <summary>
        /// Currently, the SignalR code for the JTokenValue is using a hard coded JsonSerializer,
        /// //and now using the one with the settings that I injected, so for the time being
        /// //I'm just going to take in a string and then turn it into the objectId
        /// </summary>
        /// <param name="messageId"></param>
        public void Remove(string messageId)
        {
            var objectId = new ObjectId(messageId);
            _notificationRepository.Remove(objectId);
        }

        /// <summary>
        /// We're not using the IConnected interface to trigger this event because we want to give 
        /// clientside a chance to add values to the Caller so that the IUserProvider can use any values
        /// that it needs to determine the user. We can't use  Session with SignalR because this would 
        /// eliminate the long polling non-blocking nature of SignalR, but that's where some store the 
        /// currently logged in user, this will allow us to encrypt the userId and then set it on the hub
        /// before calling the connected method so that we can get the encrypted userId in the IUserProvider
        /// decrypt it and then return it so that we can register for methods.
        /// </summary>
        /// <returns></returns>
        public Task Connected()
        {
            return RegisterAndSendMessages();
        }

        private Task RegisterAndSendMessages()
        {
            return Task.Factory.StartNew(() =>
            {
                var userId = _userProvider.GetId(Caller, Context);
                Groups.Add(Context.ConnectionId, userId); 

                var notifications = _notificationRepository.Get(userId);

                if (notifications == null || notifications.Count() == 0)
                {
                    return;
                }

                Clients[userId].notify(notifications);
            });
        }
    }
}