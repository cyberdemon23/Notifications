using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notifications.Web.Areas.Api.Models
{
    public class Notification
    {
        public int UserId { get; set; }
        public string Message { get; set; }
    }
}