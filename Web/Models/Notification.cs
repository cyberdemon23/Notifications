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
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}