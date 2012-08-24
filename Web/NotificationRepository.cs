using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Notifications.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notifications.Web
{
    public interface INotificationRepository
    {
        void Insert(Notification notification);
        IEnumerable<Notification> Get(string userName);

    }
    public class NotificationRepository : INotificationRepository
    {
        private readonly MongoCollection<Notification> _collection;

        public NotificationRepository()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var server = MongoServer.Create(connectionString);
            var database = server.GetDatabase("Notifications");
            _collection = database.GetCollection<Notification>("Notifications");
        }

        public void Insert(Notification entity)
        {
            _collection.Insert(entity);
        }

        public IEnumerable<Notification> Get(string userName)
        {
            var query = Query.EQ("UserName", userName);
            var messages = _collection.Find(query);
            return messages;
        }
    }
}