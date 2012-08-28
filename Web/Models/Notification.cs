using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notifications.Web.Models
{
    public class Notification
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
    }
}